using Acme.ChatApp.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace Acme.ChatApp.Users
{
    [Authorize]
    public class UserAppService : ApplicationService
    {
        private readonly ICurrentUser _currentUser;
        private readonly ChatAppDbContext _context;
        public UserAppService(ICurrentUser currentUser, ChatAppDbContext dbContext)
        {
            _currentUser = currentUser;
            _context = dbContext;
        }
        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var CurrentUser = _currentUser.GetId();

            var users = await _context.Users.Where(u => u.Id != CurrentUser && u.UserName != "admin").ToListAsync();

            return ObjectMapper.Map<List<IdentityUser>,List<UserDto>>(users);

        }
    }
}
