using Volo.Abp.Modularity;

namespace Acme.ChatApp;

[DependsOn(
    typeof(ChatAppApplicationModule),
    typeof(ChatAppDomainTestModule)
    )]
public class ChatAppApplicationTestModule : AbpModule
{

}
