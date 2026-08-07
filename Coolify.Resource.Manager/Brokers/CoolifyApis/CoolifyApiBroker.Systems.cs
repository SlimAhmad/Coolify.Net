// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Systems;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        public async ValueTask<ExternalSystemInfo> GetVersionAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalSystemInfo>("version", cancellationToken);

        public async ValueTask<bool> GetHealthCheckAsync(
            CancellationToken cancellationToken = default) =>
                await GetBooleanAsync("healthcheck", cancellationToken);

        public async ValueTask<bool> GetEnableApiAsync(
            CancellationToken cancellationToken = default) =>
                await GetBooleanAsync("enable", cancellationToken);

        public async ValueTask<bool> GetDisableApiAsync(
            CancellationToken cancellationToken = default) =>
                await GetBooleanAsync("disable", cancellationToken);
    }
}
