// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Systems;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<ExternalSystemInfo> GetVersionAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> GetHealthCheckAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> GetEnableApiAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> GetDisableApiAsync(CancellationToken cancellationToken = default);
    }
}
