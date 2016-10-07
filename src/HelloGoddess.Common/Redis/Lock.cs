using System;
using StackExchange.Redis;

namespace HelloGoddess.Common.Redis
{
    public class Lock
    {
        public Lock(RedisKey resource, RedisValue val, TimeSpan validity)
        {
            Resource = resource;
            Value = val;
            Validity = validity;
        }

        public RedisKey Resource { get; private set; }
        public RedisValue Value { get; private set; }
        public TimeSpan Validity { get; private set; }
    }
}