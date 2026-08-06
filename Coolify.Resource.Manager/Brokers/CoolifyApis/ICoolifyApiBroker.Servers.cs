// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Servers;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalServer>> GetAllServersAsync();
        ValueTask<ExternalServer> GetServerByUuidAsync(string serverUuid);
        ValueTask<ExternalServer> PostServerAsync(ExternalServer server);
        ValueTask<ExternalServer> PatchServerAsync(ExternalServer server);
        ValueTask DeleteServerAsync(string serverUuid);
        ValueTask<ExternalServer> GetValidateServerAsync(string serverUuid);
        ValueTask<IEnumerable<object>> GetServerResourcesAsync(string serverUuid);
        ValueTask<IEnumerable<string>> GetServerDomainsAsync(string serverUuid);
    }
}
