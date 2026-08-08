// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;

namespace Coolify.Net.Clients.Servers
{
    /// <summary>Defines the contract for managing Coolify servers.</summary>
    public interface IServerClient
    {
        /// <summary>Retrieves all servers accessible by the configured team.</summary>
        /// <exception cref="Exceptions.ServerClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.ServerClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.ServerClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<Server>> RetrieveAllServersAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a server by its UUID.</summary>
        ValueTask<Server> RetrieveServerByUuidAsync(string serverUuid, CancellationToken cancellationToken = default);

        /// <summary>Provisions a new server.</summary>
        ValueTask<Server> AddServerAsync(Server server, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing server configuration.</summary>
        ValueTask<Server> ModifyServerAsync(Server server, CancellationToken cancellationToken = default);

        /// <summary>Deprovisions and removes a server.</summary>
        ValueTask RemoveServerAsync(string serverUuid, CancellationToken cancellationToken = default);

        /// <summary>Triggers SSH / Docker reachability validation.</summary>
        ValueTask<Server> RetrieveServerValidationAsync(string serverUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists all resources deployed on a server.</summary>
        ValueTask<IEnumerable<object>> RetrieveServerResourcesAsync(string serverUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists all domains across resources on a server.</summary>
        ValueTask<IEnumerable<string>> RetrieveServerDomainsAsync(string serverUuid, CancellationToken cancellationToken = default);
    }
}
