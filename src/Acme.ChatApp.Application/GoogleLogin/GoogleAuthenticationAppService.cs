using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Acme.ChatApp.GoogleLogin
{
    //[Authorize]
    //public class GoogleAuthenticationAppService:ApplicationService,ITransientDependency
    //{
    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly IConfiguration _configuration;

    //    public GoogleAuthenticationAppService(UserManager<IdentityUser> userManager,IConfiguration configuration)
    //    {
    //        _userManager = userManager;
    //        _configuration = configuration;
    //    }

        //    public async Task<IdentityUser> AuthenticateGoogleUserAsync(ExternalAuthDto request)
        //    {
        //        if (!IsValid(request))
        //        {
        //            throw new UserFriendlyException("Invalid input data.");
        //        }

        //        var user = (await AuthenticateAndCreateUserAsync(request));
        //        //var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));
        //        return user;
        //    }

        //    private async Task<IdentityUser> AuthenticateAndCreateUserAsync(ExternalAuthDto request)
        //    {
        //        var payload = await ValidateAsync(request.IdToken, new ValidationSettings
        //        {
        //            Audience = new[] { _configuration["Authentication:Google:ClientId"] }
        //        });

        //        var user = await _userManager.FindByLoginAsync(ExternalAuthDto.PROVIDER, payload.Subject);

        //        if (user == null)
        //        {
        //            user = await _userManager.FindByEmailAsync(payload.Email);

        //            if (user == null)
        //            {
        //                user = new IdentityUser
        //                {
        //                    Email = payload.Email,
        //                    UserName = payload.GivenName,
        //                    Id = payload.Subject,
        //                };
        //            }

        //            var userName = await _userManager.FindByNameAsync(payload.GivenName);
        //            if (userName != null)
        //            {
        //                string newUserName = payload.GivenName;
        //                int count = 1;
        //                while (userName != null)
        //                {
        //                    newUserName = $"{payload.GivenName}{count:D2}";
        //                    userName = await _userManager.FindByNameAsync(newUserName);
        //                    count++;
        //                }
        //                user.UserName = newUserName;
        //                await _userManager.UpdateAsync(user);
        //            }
        //            await _userManager.CreateAsync(user);
        //            var info = new UserLoginInfo(ExternalAuthDto.PROVIDER, payload.Subject, ExternalAuthDto.PROVIDER.ToUpperInvariant());
        //            await _userManager.AddLoginAsync(user, info);
        //        }

        //        return user;
        //    }

        //    private string CreateToken(IdentityUser user)
        //    {
        //        List<Claim> claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Email, user.Email),
        //            new Claim(ClaimTypes.NameIdentifier, user.Id),
        //             //new Claim("UserId", user.UserID.ToString())
        //            //new Claim(ClaimTypes.Role,"User")
        //        };

        //        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        //        var token = new JwtSecurityToken
        //            (
        //                claims: claims,
        //                expires: DateTime.Now.AddDays(1),
        //                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        //            );

        //        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //        return jwt;

        //    }

        //    private bool IsValid(ExternalAuthDto request)
        //    {
        //        return !string.IsNullOrWhiteSpace(request.IdToken);
        //    }


        //public class GoogleAuthenticationProvider : ExternalLoginInfo
        //{
        //    public override string Name => "Google";

        //    public GoogleAuthenticationProvider(IServiceProvider serviceProvider)
        //        : base(serviceProvider)
        //    {
        //    }

        //    public override async Task<ExternalLoginUserInfo> GetUserInfoAsync(string idToken)
        //    {
        //        // Use Google's API to validate the idToken and fetch user information
        //        // You may need to use a library like Google.Apis.Auth
        //        // Extract user information (e.g., email, name) from the response

        //        // Create an ExternalLoginUserInfo object with user information
        //        var userInfo = new ExternalLoginUserInfo
        //        {
        //            Name = "John Doe",
        //            Email = "john.doe@example.com"
        //        };

        //        return userInfo;
        //    }
        //}
    //}
}
