using Acme.ChatApp.ApiLogsDto;
using Acme.ChatApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;
using static Volo.Abp.Identity.IdentityPermissions;

namespace Acme.ChatApp.Logs
{
    [Authorize]
    public class ApiLogs : ApplicationService
    {
        private readonly ChatAppDbContext _context;

        public ApiLogs(ChatAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApiLog>> GetApiLogs()
        {
            var logs=await _context.ApiLogs.ToListAsync();
            return logs;
        }

    }
}
