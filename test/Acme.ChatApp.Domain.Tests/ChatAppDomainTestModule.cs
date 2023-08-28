using Acme.ChatApp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Acme.ChatApp;

[DependsOn(
    typeof(ChatAppEntityFrameworkCoreTestModule)
    )]
public class ChatAppDomainTestModule : AbpModule
{

}
