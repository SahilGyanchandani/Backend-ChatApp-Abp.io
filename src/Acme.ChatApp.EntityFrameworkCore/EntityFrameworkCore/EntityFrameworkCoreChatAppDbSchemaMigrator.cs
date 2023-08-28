using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.ChatApp.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.ChatApp.EntityFrameworkCore;

public class EntityFrameworkCoreChatAppDbSchemaMigrator
    : IChatAppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreChatAppDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ChatAppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ChatAppDbContext>()
            .Database
            .MigrateAsync();
    }
}
