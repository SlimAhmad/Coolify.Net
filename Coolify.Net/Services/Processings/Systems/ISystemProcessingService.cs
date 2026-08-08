// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems;

namespace Coolify.Net.Services.Processings.Systems
{
    public interface ISystemProcessingService
    {
        ValueTask<SystemInfo> RetrieveVersionAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> CheckHealthAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> EnableApiAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> DisableApiAsync(CancellationToken cancellationToken = default);
    }
}
