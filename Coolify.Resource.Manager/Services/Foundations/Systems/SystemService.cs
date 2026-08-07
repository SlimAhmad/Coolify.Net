// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Systems;
using Coolify.Resource.Manager.Models.Foundations.Systems;

namespace Coolify.Resource.Manager.Services.Foundations.Systems
{
    public partial class SystemService : ISystemService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public SystemService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<SystemInfo> RetrieveVersionAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    ExternalSystemInfo externalSystemInfo =
                        await this.coolifyApiBroker.GetVersionAsync(cancellationToken);

                    return ConvertToSystemInfo(externalSystemInfo);
                });

        public ValueTask<bool> CheckHealthAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.coolifyApiBroker.GetHealthCheckAsync(cancellationToken);
                });

        public ValueTask<bool> EnableApiAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.coolifyApiBroker.GetEnableApiAsync(cancellationToken);
                });

        public ValueTask<bool> DisableApiAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.coolifyApiBroker.GetDisableApiAsync(cancellationToken);
                });

        // ---- Conversion helpers ----

        private static SystemInfo ConvertToSystemInfo(ExternalSystemInfo externalSystemInfo) =>
            new SystemInfo
            {
                Version = externalSystemInfo.Version
            };
    }
}
