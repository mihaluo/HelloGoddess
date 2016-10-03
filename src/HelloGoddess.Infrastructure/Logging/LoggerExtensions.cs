using System;
using Microsoft.Extensions.Logging;

namespace HelloGoddess.Infrastructure.Logging
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/>.
    /// </summary>
    public static class LoggerExtensions
    {
        public static void Log(this ILogger logger, LogSeverity severity, string message)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.LogError(message);
                    break;
                case LogSeverity.Error:
                    logger.LogError(message);
                    break;
                case LogSeverity.Warn:
                    logger.LogWarning(message);
                    break;
                case LogSeverity.Info:
                    logger.LogInformation(message);
                    break;
                case LogSeverity.Debug:
                    logger.LogDebug(message);
                    break;
                default:
                    throw new AbpException("Unknown LogSeverity value: " + severity);
            }
        }

        public static void Log(this ILogger logger, LogSeverity severity, string message, Exception exception)
        {
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.LogError(message, exception);
                    break;
                case LogSeverity.Error:
                    logger.LogError(message, exception);
                    break;
                case LogSeverity.Warn:
                    logger.LogWarning(message, exception);
                    break;
                case LogSeverity.Info:
                    logger.LogInformation(message, exception);
                    break;
                case LogSeverity.Debug:
                    logger.LogDebug(message, exception);
                    break;
                default:
                    throw new AbpException("Unknown LogSeverity value: " + severity);
            }
        }

        public static void Log(this ILogger logger, LogSeverity severity, Func<string> messageFactory)
        {
            var msg = messageFactory();
            switch (severity)
            {
                case LogSeverity.Fatal:
                    logger.LogError(msg);
                    break;
                case LogSeverity.Error:
                    logger.LogError(msg);
                    break;
                case LogSeverity.Warn:
                    logger.LogWarning(msg);
                    break;
                case LogSeverity.Info:
                    logger.LogInformation(msg);
                    break;
                case LogSeverity.Debug:
                    logger.LogDebug(msg);
                    break;
                default:
                    throw new AbpException("Unknown LogSeverity value: " + severity);
            }
        }
    }
}