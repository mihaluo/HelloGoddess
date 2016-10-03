using System;

namespace HelloGoddess.Infrastructure
{
    /// <summary>
    /// This exception is thrown if a problem on ABP initialization progress.
    /// </summary>
    public class AbpInitializationException : AbpException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AbpInitializationException()
        {

        }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        public AbpInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public AbpInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
