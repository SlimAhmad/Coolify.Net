// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Applications;
using Coolify.Net.Clients.CoolifyServices;
using Coolify.Net.Clients.Databases;
using Coolify.Net.Clients.Deployments;
using Coolify.Net.Clients.PrivateKeys;
using Coolify.Net.Clients.Projects;
using Coolify.Net.Clients.Servers;
using Coolify.Net.Clients.Systems;
using Coolify.Net.Clients.Teams;

namespace Coolify.Net.Clients.Coolify.Net
{
    /// <summary>
    /// Defines the contract for the main Coolify client, providing access to every Coolify
    /// resource client (servers, projects, applications, databases, and more) from a single object.
    /// </summary>
    public interface ICoolifyClient
    {
        /// <summary>Gets the client for managing Coolify servers.</summary>
        IServerClient Servers { get; }

        /// <summary>Gets the client for managing Coolify projects and their environments.</summary>
        IProjectClient Projects { get; }

        /// <summary>Gets the client for managing Coolify applications.</summary>
        IApplicationClient Applications { get; }

        /// <summary>Gets the client for managing Coolify databases.</summary>
        IDatabaseClient Databases { get; }

        /// <summary>Gets the client for managing Coolify one-click services.</summary>
        ICoolifyServiceClient CoolifyServices { get; }

        /// <summary>Gets the client for managing Coolify deployments.</summary>
        IDeploymentClient Deployments { get; }

        /// <summary>Gets the client for reading Coolify teams and their members.</summary>
        ITeamClient Teams { get; }

        /// <summary>Gets the client for managing Coolify private keys.</summary>
        IPrivateKeyClient PrivateKeys { get; }

        /// <summary>Gets the client for reading Coolify instance/system status.</summary>
        ISystemClient System { get; }
    }
}
