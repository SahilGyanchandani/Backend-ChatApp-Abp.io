using System;
using System.Collections.Generic;
using System.Text;
using Acme.ChatApp.Localization;
using Volo.Abp.Application.Services;

namespace Acme.ChatApp;

/* Inherit your application services from this class.
 */
public abstract class ChatAppAppService : ApplicationService
{
    protected ChatAppAppService()
    {
        LocalizationResource = typeof(ChatAppResource);
    }
}
