using Acme.ChatApp.RedisConn;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.ChatApp.RedisConn
{
    public class RedisConnection : IRedisConnection
    {
        private readonly IDatabase _redis;

        public RedisConnection(ConnectionMultiplexer redisConnection)
        {
            _redis = redisConnection.GetDatabase();
        }
        public async Task AddConnectionAsync(string userId, string connectionId)
        {
            await _redis.StringSetAsync(userId, connectionId);
        }
        public async Task<string> GenConnIdAsync(string userId)
        {
            return await _redis.StringGetAsync(userId);
        }
    }
}
