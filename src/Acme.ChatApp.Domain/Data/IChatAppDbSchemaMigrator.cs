using System.Threading.Tasks;

namespace Acme.ChatApp.Data;

public interface IChatAppDbSchemaMigrator
{
    Task MigrateAsync();
}
