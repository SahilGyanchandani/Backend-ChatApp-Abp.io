using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Acme.ChatApp.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
