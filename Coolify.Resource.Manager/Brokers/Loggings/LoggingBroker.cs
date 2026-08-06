// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace Coolify.Resource.Manager.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public ValueTask LogErrorAsync(Exception exception)
        {
            this.logger.LogError(exception, exception.Message);

            return ValueTask.CompletedTask;
        }

        public ValueTask LogCriticalAsync(Exception exception)
        {
            this.logger.LogCritical(exception, exception.Message);

            return ValueTask.CompletedTask;
        }
    }
}
