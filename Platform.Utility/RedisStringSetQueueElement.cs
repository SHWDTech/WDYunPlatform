using System;
using StackExchange.Redis;

namespace SHWDTech.Platform.Utility
{
    public class RedisStringSetQueueElement
    {
        public RedisKey Key { get; set; }

        public RedisValue Value { get; set; }

        public TimeSpan? Expiry { get; set; }

        public When When { get; set; }

        public CommandFlags Flags { get; set; }
    }
}
