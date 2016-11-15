using System;
using HelloGoddess.Common.Util;
using StackExchange.Redis;

namespace HelloGoddess.Common.Redis
{
    public static class RedisHelper
    {
        private static readonly ConnectionMultiplexer ConnectionMultiplexer;
        private static readonly IDatabase Database;
        static RedisHelper()
        {
            var redisConnectionString = $"{IpHelper.GetIp("redis")}:6379,connectTimeout=5000,SyncTimeout=60000";
            ConnectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
            Database = ConnectionMultiplexer.GetDatabase();
        }

        private static IDatabase GetDatabase()
        {
            return Database;
        }

        public static double StringIncrement(string key, double value)
        {
            return GetDatabase().StringIncrement(key, value);
        }

        public static RedisValue StringGet(string key)
        {
            return GetDatabase().StringGet(key);
        }

        public static bool KeyExists(string key)
        {
            return GetDatabase().KeyExists(key);
        }

        public static bool Remove(string key)
        {
            return GetDatabase().KeyDelete(key);
        }

        public static T Get<T>(string key)
        {
            var redisValue = GetDatabase().StringGet(key);
            if (redisValue.IsNull)
            {
                return default(T);
            }

            return ((string)redisValue).ToObj<T>();
        }

        public static bool Set<T>(string key, T obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return GetDatabase().StringSet(key, obj.ToJson());
        }

    }
}