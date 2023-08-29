using Acme.ChatApp.ApiLogsDto;
using Acme.ChatApp.Users;
using AutoMapper;
using Volo.Abp.Identity;

namespace Acme.ChatApp;

public class ChatAppApplicationAutoMapperProfile : Profile
{
    public ChatAppApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentitySecurityLog,LogsDto>();
    }
}
