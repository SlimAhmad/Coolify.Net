// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Foundations.Systems;
using Coolify.Net.Services.Foundations.Systems;

namespace Coolify.Net.Services.Processings.Systems
{
    public partial class SystemProcessingService : ISystemProcessingService
    {
        private readonly ISystemService systemService;
        private readonly ILoggingBroker loggingBroker;

        public SystemProcessingService(
            ISystemService systemService,
            ILoggingBroker loggingBroker)
        {
            this.systemService = systemService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<SystemInfo> RetrieveVersionAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.systemService.RetrieveVersionAsync(cancellationToken);
                });

        public ValueTask<bool> CheckHealthAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.systemService.CheckHealthAsync(cancellationToken);
                });

        public ValueTask<bool> EnableApiAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.systemService.EnableApiAsync(cancellationToken);
                });

        public ValueTask<bool> DisableApiAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.systemService.DisableApiAsync(cancellationToken);
                });
    }
}
