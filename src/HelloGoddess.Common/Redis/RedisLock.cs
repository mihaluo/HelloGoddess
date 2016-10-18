using System;
using System.Collections.Generic;
using System.Linq;
using HelloGoddess.Common.Util;
using StackExchange.Redis;

namespace HelloGoddess.Common.Redis
{
    public class RedisLock
    {
        private static IList<ConnectionMultiplexer> _connectionMultiplexers;

        protected static IList<ConnectionMultiplexer> ConnectionMultiplexers
        {
            get
            {
                if (_connectionMultiplexers == null)
                {
                    var redisConnectionString = $"{IpHelper.GetIp("redis")}:6379,connectTimeout=5000,SyncTimeout=5000";
                    _connectionMultiplexers = new List<ConnectionMultiplexer> { ConnectionMultiplexer.Connect(redisConnectionString) };
                }
                return _connectionMultiplexers;
            }
        }

        /// <summary>
        /// 多重锁
        /// </summary>
        /// <param name="lockId">锁ID</param>
        /// <param name="action">主处理Action</param>
        /// <param name="actionExecuted">主处理Action执行完成后执行的Action</param>
        public static void SingleLock(string lockId, Action action, Action actionExecuted)
        {
            //Console.WriteLine("start exe lock");

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var dlm = new Redlock(ConnectionMultiplexers.ToArray());

            var beginProcessTime = DateTime.Now;

            var lockObject = GetLock(lockId, dlm);

            //执行处理
            ExecuteAction(action);

            //Action 处理时间大于过期时间
            if ((DateTime.Now - beginProcessTime).TotalMilliseconds > GetExpireTime().TotalMilliseconds)
            {
                throw new RedisLockException("处理超时");
            }
            actionExecuted?.Invoke();

            //释放锁
            dlm.Unlock(lockObject);

            //Console.WriteLine("end exe lock");

        }

        /// <summary>
        /// 多重锁
        /// </summary>
        /// <param name="lockIds">锁Ids</param>
        /// <param name="action">主处理Action</param>
        /// <param name="actionExecuted">主处理Action执行完成后执行的Action</param>
        public static void MultipleLock(IList<string> lockIds, Action action, Action actionExecuted)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (lockIds.Count <= 0) throw new ArgumentException("lockIds不能为空");

            var dlm = new Redlock(ConnectionMultiplexers.ToArray());

            var beginProcessTime = DateTime.Now;

            IList<Lock> locks = lockIds.Select(lockId => GetLock(lockId, dlm)).ToList();


            //执行处理
            ExecuteAction(action);

            //Action 处理时间大于过期时间
            if ((DateTime.Now - beginProcessTime).TotalMilliseconds > GetExpireTime().TotalMilliseconds)
            {
                throw new RedisLockException("处理超时");
            }

            actionExecuted?.Invoke();

            //释放锁
            foreach (var @lock in locks)
            {
                dlm.Unlock(@lock);
            }
        }

        private static void ExecuteAction(Action action)
        {

            action();

        }


        private static Lock GetLock(string lockId, Redlock dlm)
        {
            //获取过期时间
            var expireTime = GetExpireTime();
            //获取 锁的获取最长时间
            var getLockTimeOut = GetGetLockTimeOut();

            var startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < getLockTimeOut.TotalMilliseconds)
            {
                //获取锁
                Lock lockObject;
                var locked = dlm.Lock(lockId, expireTime, out lockObject);
                if (locked)
                {
                    return lockObject;
                }
            }

            throw new RedisLockException("获取锁超时");
        }

        /// <summary>
        ///     取锁的最长时间
        /// </summary>
        /// <returns></returns>
        private static TimeSpan GetGetLockTimeOut()
        {
            var seconds = 30;

            return new TimeSpan(0, 0, 0, seconds);
        }

        /// <summary>
        ///     锁的过期时间
        /// </summary>
        /// <returns></returns>
        private static TimeSpan GetExpireTime()
        {
            var seconds = 30;

            return new TimeSpan(0, 0, 0, seconds);
        }

        public class RedisLockException : Exception
        {
            public RedisLockException()
            {
            }

            public RedisLockException(string msg) : base(msg)
            {
            }
        }
    }
}