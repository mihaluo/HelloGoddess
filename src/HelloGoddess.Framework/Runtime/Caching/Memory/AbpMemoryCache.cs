using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace HelloGoddess.Infrastructure.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICache"/> to work with <see cref="MemoryCache"/>.
    /// </summary>
    public class AbpMemoryCache : CacheBase
    {
        private MemoryCache _memoryCache;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Unique name of the cache</param>
        public AbpMemoryCache(string name)
            : base(name)

        {
            _memoryCache = new MemoryCache(Options.Create<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new AbpException("Can not insert null values to the cache!");
            }

            var memoryCacheEntryOptions = new MemoryCacheEntryOptions();

            if (absoluteExpireTime != null)
            {

                memoryCacheEntryOptions.AbsoluteExpiration = DateTimeOffset.Now.Add(absoluteExpireTime.Value);
            }
            else if (slidingExpireTime != null)
            {
                memoryCacheEntryOptions.SlidingExpiration = slidingExpireTime.Value;
            }
            else if (DefaultAbsoluteExpireTime != null)
            {
                memoryCacheEntryOptions.AbsoluteExpiration = DateTimeOffset.Now.Add(DefaultAbsoluteExpireTime.Value);
            }
            else
            {
                memoryCacheEntryOptions.SlidingExpiration = DefaultSlidingExpireTime;
            }

            _memoryCache.Set(key, value, memoryCacheEntryOptions);
        }

        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(Options.Create<MemoryCacheOptions>(new MemoryCacheOptions()));
        }

        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}