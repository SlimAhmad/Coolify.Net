// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;

namespace Coolify.Net.Services.Processings.Servers
{
    public interface IServerProcessingService
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
