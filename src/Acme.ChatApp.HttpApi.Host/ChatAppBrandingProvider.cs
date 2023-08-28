using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme.ChatApp;

[Dependency(ReplaceServices = true)]
public class ChatAppBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ChatApp";
}
