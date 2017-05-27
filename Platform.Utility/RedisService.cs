using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SHWDTech.Platform.Utility
{
    public class RedisService
    {
        private static readonly IDatabase RedisDatabase;

        private static readonly Queue<RedisStringSetQueueElement> StringSetQueue = new Queue<RedisStringSetQueueElement>();

        static RedisService()
        {
            RedisDatabase = ConnectionMultiplexer.Connect("localhost").GetDatabase();
            Task.Factory.StartNew(ProcessQueue);
        }

        public static IDatabase GetRedisDatabase() => RedisDatabase;

        public static void StringSetInQueue(RedisKey key, RedisValue value, TimeSpan? expiry = null, When when = When.Always, CommandFlags flags = CommandFlags.None)
        {
            var element = new RedisStringSetQueueElement
            {
                Key = key,
                Value = value,
                Expiry = expiry,
                When = when,
                Flags = flags
            };

            StringSetQueue.Enqueue(element);
        }

        public static RedisValue MakeSureStringGet(RedisKey key)
        {
            var geted = false;
            RedisValue value = "";
            while (!geted)
            {
                try
                {
                    value = RedisDatabase.StringGet(key);
                    geted = true;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return value;
        }

        private static void ProcessQueue()
        {
            while (true)
            {
                if (StringSetQueue.Count <= 0)
                {
                    Thread.Sleep(500);
                    continue;
                }

                var set = StringSetQueue.Dequeue();
                try
                {
                    RedisDatabase.StringSet(set.Key, set.Value, set.Expiry, set.When, set.Flags);
                }
                catch (Exception)
                {
                    StringSetQueue.Enqueue(set);
                }
            }
        }
    }
}
