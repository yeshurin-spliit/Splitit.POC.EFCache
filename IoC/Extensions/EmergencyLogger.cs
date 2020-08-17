using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace EFCache.POC.IoC.Extensions
{
    public class EmergencyLogger
    {
        public static void LogWarn(Exception exception, string additionalMessage)
        {
            var loggingProviders = new List<ILoggerProvider> { new EventLogLoggerProvider() };
            ILoggerFactory loggerFactory = new LoggerFactory(loggingProviders);
            var logger = loggerFactory.CreateLogger<EmergencyLogger>();
            logger.LogWarning(new EventId(1309), exception, $"{additionalMessage} ------ {exception.Message}");
            if (exception.InnerException != null)
            {
                logger.LogWarning(new EventId(1309), exception.InnerException, $"Inner Exception {exception.InnerException.Message}");
            }
        }
    }
}