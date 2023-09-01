using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Acme.ChatApp.RedisConn
{
    public interface IRedisConnection
    {
        Task AddConnectionAsync(string userId, string connectionId);
        Task<string> GenConnIdAsync(string userId);
    }

}
