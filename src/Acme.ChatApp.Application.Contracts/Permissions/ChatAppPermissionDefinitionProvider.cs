using Acme.ChatApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.ChatApp.Permissions;

public class ChatAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ChatAppPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ChatAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ChatAppResource>(name);
    }
}
