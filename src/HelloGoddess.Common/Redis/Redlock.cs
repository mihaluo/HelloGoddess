using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using StackExchange.Redis;

namespace HelloGoddess.Common.Redis
{
    public class Redlock
    {
        private const int DefaultRetryCount = 3;
        private const double ClockDriveFactor = 0.01;

        /// <summary>
        ///     String containing the Lua unlock script.
        /// </summary>
        private const string UnlockScript = @"
            if redis.call(""get"",KEYS[1]) == ARGV[1] then
                return redis.call(""del"",KEYS[1])
            else
                return 0
            end";

        private readonly TimeSpan _defaultRetryDelay = new TimeSpan(0, 0, 0, 0, 200);

        protected Dictionary<string, ConnectionMultiplexer> RedisMasterDictionary =
            new Dictionary<string, ConnectionMultiplexer>();

        public Redlock(params ConnectionMultiplexer[] list)
        {
            foreach (var item in list)
                RedisMasterDictionary.Add(item.GetEndPoints().First().ToString(), item);
        }

        protected int Quorum
        {
            get { return (RedisMasterDictionary.Count/2) + 1; }
        }

        protected static byte[] CreateUniqueLockId()
        {
            return Guid.NewGuid().ToByteArray();
        }
        
        protected bool LockInstance(ConnectionMultiplexer redis, string resource, byte[] val, TimeSpan ttl)
        {
            bool succeeded;
            try
            {
                succeeded = redis.GetDatabase().StringSet(resource, val, ttl, When.NotExists);
            }
            catch (Exception e)
            {
                succeeded = false;
            }
            return succeeded;
        }
        
        protected void UnlockInstance(ConnectionMultiplexer redis, string resource, byte[] val)
        {
            RedisKey[] key = {resource};
            RedisValue[] values = {val};
            RedisResult redisResult = redis.GetDatabase().ScriptEvaluate(
                UnlockScript,
                key,
                values
                );
            
        }

        public bool Lock(RedisKey resource, TimeSpan ttl, out Lock lockObject)
        {
            var val = CreateUniqueLockId();
            Lock innerLock = null;
            var successfull = Retry(DefaultRetryCount, _defaultRetryDelay, () =>
            {
                try
                {
                    var n = 0;
                    var startTime = DateTime.Now;

                    // Use keys
                    for_each_redis_registered(
                        redis => { if (LockInstance(redis, resource, val, ttl)) n += 1; }
                        );

                    /*
                     * Add 2 milliseconds to the drift to account for Redis expires
                     * precision, which is 1 milliescond, plus 1 millisecond min drift 
                     * for small TTLs.        
                     */
                    var drift = Convert.ToInt32((ttl.TotalMilliseconds*ClockDriveFactor) + 2);
                    var validityTime = ttl - (DateTime.Now - startTime) - new TimeSpan(0, 0, 0, 0, drift);

                    if (n >= Quorum && validityTime.TotalMilliseconds > 0)
                    {
                        innerLock = new Lock(resource, val, validityTime);
                        return true;
                    }
                    for_each_redis_registered(
                        redis => { UnlockInstance(redis, resource, val); }
                        );
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });

            lockObject = innerLock;
            return successfull;
        }

        protected void for_each_redis_registered(Action<ConnectionMultiplexer> action)
        {
            foreach (var item in RedisMasterDictionary)
            {
                action(item.Value);
            }
        }

        protected void for_each_redis_registered(Action<string> action)
        {
            foreach (var item in RedisMasterDictionary)
            {
                action(item.Key);
            }
        }

        protected bool Retry(int retryCount, TimeSpan retryDelay, Func<bool> action)
        {
            var maxRetryDelay = (int) retryDelay.TotalMilliseconds;
            var rnd = new Random();
            var currentRetry = 0;

            while (currentRetry++ < retryCount)
            {
                if (action()) return true;
                Thread.Sleep(rnd.Next(maxRetryDelay));
            }
            return false;
        }

        public void Unlock(Lock lockObject)
        {
            for_each_redis_registered(redis => { UnlockInstance(redis, lockObject.Resource, lockObject.Value); });
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(GetType().FullName);

            sb.AppendLine("Registered Connections:");
            foreach (var item in RedisMasterDictionary)
            {
                sb.AppendLine(item.Value.GetEndPoints().First().ToString());
            }

            return sb.ToString();
        }
    }
}