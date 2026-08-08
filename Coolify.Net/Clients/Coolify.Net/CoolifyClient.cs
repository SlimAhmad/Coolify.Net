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
using Coolify.Net.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Coolify.Net.Clients.Coolify.Net
{
    /// <summary>
    /// Represents the main entry point for the Coolify.Net library, providing access
    /// to every Coolify resource client (servers, projects, applications, databases, and more)
    /// from a single object. Consumers who already run their own dependency injection container
    /// should use the <c>AddCoolifyClient</c> service collection extension instead.
    /// </summary>
    public class CoolifyClient : ICoolifyClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoolifyClient"/> class with the
        /// specified options configuration delegate.
        /// </summary>
        /// <param name="configureOptions">A delegate used to configure the
        /// <see cref="CoolifyClientOptions"/> (base URL, API token, and timeout).</param>
        public CoolifyClient(Action<CoolifyClientOptions> configureOptions)
        {
            IServiceProvider serviceProvider = ConfigureDependencies(configureOptions);
            InitializeClients(serviceProvider);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoolifyClient"/> class with the
        /// specified options.
        /// </summary>
        /// <param name="options">The Coolify client options (base URL, API token, and
        /// timeout).</param>
        public CoolifyClient(CoolifyClientOptions options)
            : this(configureOptions => CopyOptions(options, configureOptions))
        { }

        /// <summary>Gets the client for managing Coolify servers.</summary>
        public IServerClient Servers { get; private set; }

        /// <summary>Gets the client for managing Coolify projects and their environments.</summary>
        public IProjectClient Projects { get; private set; }

        /// <summary>Gets the client for managing Coolify applications.</summary>
        public IApplicationClient Applications { get; private set; }

        /// <summary>Gets the client for managing Coolify databases.</summary>
        public IDatabaseClient Databases { get; private set; }

        /// <summary>Gets the client for managing Coolify one-click services.</summary>
        public ICoolifyServiceClient CoolifyServices { get; private set; }

        /// <summary>Gets the client for managing Coolify deployments.</summary>
        public IDeploymentClient Deployments { get; private set; }

        /// <summary>Gets the client for reading Coolify teams and their members.</summary>
        public ITeamClient Teams { get; private set; }

        /// <summary>Gets the client for managing Coolify private keys.</summary>
        public IPrivateKeyClient PrivateKeys { get; private set; }

        /// <summary>Gets the client for reading Coolify instance/system status.</summary>
        public ISystemClient System { get; private set; }

        private void InitializeClients(IServiceProvider serviceProvider)
        {
            this.Servers = serviceProvider.GetRequiredService<IServerClient>();
            this.Projects = serviceProvider.GetRequiredService<IProjectClient>();
            this.Applications = serviceProvider.GetRequiredService<IApplicationClient>();
            this.Databases = serviceProvider.GetRequiredService<IDatabaseClient>();
            this.CoolifyServices = serviceProvider.GetRequiredService<ICoolifyServiceClient>();
            this.Deployments = serviceProvider.GetRequiredService<IDeploymentClient>();
            this.Teams = serviceProvider.GetRequiredService<ITeamClient>();
            this.PrivateKeys = serviceProvider.GetRequiredService<IPrivateKeyClient>();
            this.System = serviceProvider.GetRequiredService<ISystemClient>();
        }

        private static IServiceProvider ConfigureDependencies(Action<CoolifyClientOptions> configureOptions)
        {
            var services = new ServiceCollection();
            services.AddCoolifyClient(configureOptions);

            return services.BuildServiceProvider();
        }

        private static void CopyOptions(CoolifyClientOptions source, CoolifyClientOptions destination)
        {
            destination.BaseUrl = source.BaseUrl;
            destination.ApiToken = source.ApiToken;
            destination.Timeout = source.Timeout;
        }
    }
}
