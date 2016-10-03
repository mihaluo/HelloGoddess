using System;
using System.Threading.Tasks;

namespace HelloGoddess.Infrastructure
{
    internal class AsyncLock
    {
        public AsyncLock()
        {
        }

        internal Task<IDisposable> LockAsync()
        {
            throw new NotImplementedException();
        }
    }
}