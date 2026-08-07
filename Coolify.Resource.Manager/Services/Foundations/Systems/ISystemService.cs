// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Systems;

namespace Coolify.Resource.Manager.Services.Foundations.Systems
{
    public interface ISystemService
    {
        ValueTask<SystemInfo> RetrieveVersionAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> CheckHealthAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> EnableApiAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> DisableApiAsync(CancellationToken cancellationToken = default);
    }
}
