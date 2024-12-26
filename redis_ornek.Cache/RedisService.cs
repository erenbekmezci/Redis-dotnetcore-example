using StackExchange.Redis;
using System;

namespace redis_ornek.Cache
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer connectionMultiplexer;
      
        public RedisService(string url)
        {
            connectionMultiplexer = ConnectionMultiplexer.Connect(url);
           
        }

        public IDatabase getDb(int db)
        {
            return connectionMultiplexer.GetDatabase(db);
        }

    }
}
