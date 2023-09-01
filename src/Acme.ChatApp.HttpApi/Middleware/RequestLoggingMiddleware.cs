using Acme.ChatApp.EntityFrameworkCore;
using Acme.ChatApp.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace Acme.ChatApp.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly ICurrentUser _currentUser;
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly ChatAppDbContext _dbContext;
        public RequestLoggingMiddleware(ICurrentUser currentUser,RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, ChatAppDbContext dbContext)
        {
            _currentUser = currentUser;
            _next = next;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var requestBody = await GetRequestBody(request);


            var log = new ApiLog
            {
                IpAddress = GetIpAddress(context),
                RequestBody = requestBody,
                TimeStamp = DateTime.Now,
                userName = userName()
               
            };

            _dbContext.ApiLogs.Add(log);
            await _dbContext.SaveChangesAsync();

            await _next(context);
        }

        private string userName()
        {
            //var current = _currentUser.GetId();
            //if(current!=null)
            //{
            //    var User = _dbContext.Users.Where(m => m.Id == current).Select(m => m.UserName).FirstOrDefault();
            //    return User;
            //}
            //else
            //{
            //    return "Anonymous";
            //}
            try
            {
                var current = _currentUser.GetId();
                if (current != null)
                {
                    var User = _dbContext.Users.Where(m => m.Id == current).Select(m => m.UserName).FirstOrDefault();
                    return User;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it or perform other actions.
                _logger.LogError(ex, "Error occurred while retrieving user name.");
                return null; // or another suitable default value
            }

        }

        private async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            var body = string.Empty;

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            return body;
        }

        private string GetIpAddress(HttpContext context)
        {
            return context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        }

    }
}
