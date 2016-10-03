using Microsoft.Extensions.Logging;

namespace HelloGoddess.Infrastructure
{
    internal class NullLogger
    {
        public static ILogger Instance { get; private set; }
    }
}