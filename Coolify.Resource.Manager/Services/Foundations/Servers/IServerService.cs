// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;

namespace Coolify.Resource.Manager.Services.Foundations.Servers
{
    public interface IServerService
    {
        ValueTask<IEnumerable<Server>> RetrieveAllServersAsync(CancellationToken cancellationToken = default);
        ValueTask<Server> RetrieveServerByUuidAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<Server> AddServerAsync(Server server, CancellationToken cancellationToken = default);
        ValueTask<Server> ModifyServerAsync(Server server, CancellationToken cancellationToken = default);
        ValueTask RemoveServerAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<Server> RetrieveServerValidationAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<object>> RetrieveServerResourcesAsync(string serverUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<string>> RetrieveServerDomainsAsync(string serverUuid, CancellationToken cancellationToken = default);
    }
}
