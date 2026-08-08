// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net.Http.Headers;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Clients.Applications;
using Coolify.Net.Clients.CoolifyServices;
using Coolify.Net.Clients.Databases;
using Coolify.Net.Clients.Deployments;
using Coolify.Net.Clients.PrivateKeys;
using Coolify.Net.Clients.Projects;
using Coolify.Net.Clients.Servers;
using Coolify.Net.Clients.Systems;
using Coolify.Net.Clients.Teams;
using Coolify.Net.Services.Foundations.Applications;
using Coolify.Net.Services.Foundations.CoolifyServices;
using Coolify.Net.Services.Foundations.Databases;
using Coolify.Net.Services.Foundations.Deployments;
using Coolify.Net.Services.Foundations.PrivateKeys;
using Coolify.Net.Services.Foundations.Projects;
using Coolify.Net.Services.Foundations.Servers;
using Coolify.Net.Services.Foundations.Systems;
using Coolify.Net.Services.Foundations.Teams;
using Coolify.Net.Services.Processings.Applications;
using Coolify.Net.Services.Processings.CoolifyServices;
using Coolify.Net.Services.Processings.Databases;
using Coolify.Net.Services.Processings.Deployments;
using Coolify.Net.Services.Processings.PrivateKeys;
using Coolify.Net.Services.Processings.Projects;
using Coolify.Net.Services.Processings.Servers;
using Coolify.Net.Services.Processings.Systems;
using Coolify.Net.Services.Processings.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace Coolify.Net.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoolifyClient(
            this IServiceCollection services,
            Action<CoolifyClientOptions> configureOptions)
        {
            var options = new CoolifyClientOptions();
            configureOptions(options);

            services.AddLogging();

            services.AddHttpClient(nameof(CoolifyApiBroker), httpClient =>
            {
                httpClient.BaseAddress =
                    new Uri($"{options.BaseUrl.TrimEnd('/')}/api/v1/");

                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", options.ApiToken);

                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                httpClient.Timeout = options.Timeout;
            });

            // Brokers
            services.AddTransient<ICoolifyApiBroker, CoolifyApiBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();

            // Foundation Services
            services.AddTransient<IServerService, ServerService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddTransient<ICoolifyServiceService, CoolifyServiceService>();
            services.AddTransient<IDeploymentService, DeploymentService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<IPrivateKeyService, PrivateKeyService>();
            services.AddTransient<ISystemService, SystemService>();

            // Processing Services
            services.AddTransient<IServerProcessingService, ServerProcessingService>();
            services.AddTransient<IProjectProcessingService, ProjectProcessingService>();
            services.AddTransient<IApplicationProcessingService, ApplicationProcessingService>();
            services.AddTransient<IDatabaseProcessingService, DatabaseProcessingService>();
            services.AddTransient<ICoolifyServiceProcessingService, CoolifyServiceProcessingService>();
            services.AddTransient<IDeploymentProcessingService, DeploymentProcessingService>();
            services.AddTransient<ITeamProcessingService, TeamProcessingService>();
            services.AddTransient<IPrivateKeyProcessingService, PrivateKeyProcessingService>();
            services.AddTransient<ISystemProcessingService, SystemProcessingService>();

            // Clients (public surface)
            services.AddTransient<IServerClient, ServerClient>();
            services.AddTransient<IProjectClient, ProjectClient>();
            services.AddTransient<IApplicationClient, ApplicationClient>();
            services.AddTransient<IDatabaseClient, DatabaseClient>();
            services.AddTransient<ICoolifyServiceClient, CoolifyServiceClient>();
            services.AddTransient<IDeploymentClient, DeploymentClient>();
            services.AddTransient<ITeamClient, TeamClient>();
            services.AddTransient<IPrivateKeyClient, PrivateKeyClient>();
            services.AddTransient<ISystemClient, SystemClient>();

            return services;
        }
    }
}
