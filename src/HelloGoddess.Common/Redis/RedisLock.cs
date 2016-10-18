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
        /// ������
        /// </summary>
        /// <param name="lockId">��ID</param>
        /// <param name="action">������Action</param>
        /// <param name="actionExecuted">������Actionִ����ɺ�ִ�е�Action</param>
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

            //ִ�д���
            ExecuteAction(action);

            //Action ����ʱ����ڹ���ʱ��
            if ((DateTime.Now - beginProcessTime).TotalMilliseconds > GetExpireTime().TotalMilliseconds)
            {
                throw new RedisLockException("����ʱ");
            }
            actionExecuted?.Invoke();

            //�ͷ���
            dlm.Unlock(lockObject);

            //Console.WriteLine("end exe lock");

        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="lockIds">��Ids</param>
        /// <param name="action">������Action</param>
        /// <param name="actionExecuted">������Actionִ����ɺ�ִ�е�Action</param>
        public static void MultipleLock(IList<string> lockIds, Action action, Action actionExecuted)
        {
            if (action == null) throw new ArgumentNullException("action");
            if (lockIds.Count <= 0) throw new ArgumentException("lockIds����Ϊ��");

            var dlm = new Redlock(ConnectionMultiplexers.ToArray());

            var beginProcessTime = DateTime.Now;

            IList<Lock> locks = lockIds.Select(lockId => GetLock(lockId, dlm)).ToList();


            //ִ�д���
            ExecuteAction(action);

            //Action ����ʱ����ڹ���ʱ��
            if ((DateTime.Now - beginProcessTime).TotalMilliseconds > GetExpireTime().TotalMilliseconds)
            {
                throw new RedisLockException("����ʱ");
            }

            actionExecuted?.Invoke();

            //�ͷ���
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
            //��ȡ����ʱ��
            var expireTime = GetExpireTime();
            //��ȡ ���Ļ�ȡ�ʱ��
            var getLockTimeOut = GetGetLockTimeOut();

            var startTime = DateTime.Now;

            while ((DateTime.Now - startTime).TotalMilliseconds < getLockTimeOut.TotalMilliseconds)
            {
                //��ȡ��
                Lock lockObject;
                var locked = dlm.Lock(lockId, expireTime, out lockObject);
                if (locked)
                {
                    return lockObject;
                }
            }

            throw new RedisLockException("��ȡ����ʱ");
        }

        /// <summary>
        ///     ȡ�����ʱ��
        /// </summary>
        /// <returns></returns>
        private static TimeSpan GetGetLockTimeOut()
        {
            var seconds = 30;

            return new TimeSpan(0, 0, 0, seconds);
        }

        /// <summary>
        ///     ���Ĺ���ʱ��
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