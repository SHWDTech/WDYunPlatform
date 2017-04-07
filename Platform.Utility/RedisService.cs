using StackExchange.Redis;

namespace SHWDTech.Platform.Utility
{
    public class RedisService
    {
        private static readonly IDatabase RedisDatabase;

        static RedisService()
        {
            RedisDatabase = ConnectionMultiplexer.Connect("localhost").GetDatabase();
        }

        public static IDatabase GetREdisDatabase() => RedisDatabase;
    }
}
