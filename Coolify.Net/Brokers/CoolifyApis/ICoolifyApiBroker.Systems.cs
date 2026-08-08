// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Systems;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<ExternalSystemInfo> GetVersionAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> GetHealthCheckAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> GetEnableApiAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> GetDisableApiAsync(CancellationToken cancellationToken = default);
    }
}
