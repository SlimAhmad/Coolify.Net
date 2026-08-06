// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Servers;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalServer>> GetAllServersAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalServer> GetServerByUuidAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalServer> PostServerAsync(ExternalServer server, CancellationToken cancellationToken = default);
        ValueTask<ExternalServer> PatchServerAsync(ExternalServer server, CancellationToken cancellationToken = default);
        ValueTask DeleteServerAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalServer> GetValidateServerAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<object>> GetServerResourcesAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<string>> GetServerDomainsAsync(string serverUuid, CancellationToken cancellationToken = default);
    }
}
