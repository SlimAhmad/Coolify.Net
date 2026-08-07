// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net.Http.Headers;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Clients.Applications;
using Coolify.Resource.Manager.Clients.CoolifyServices;
using Coolify.Resource.Manager.Clients.Databases;
using Coolify.Resource.Manager.Clients.Deployments;
using Coolify.Resource.Manager.Clients.Projects;
using Coolify.Resource.Manager.Clients.Servers;
using Coolify.Resource.Manager.Clients.Teams;
using Coolify.Resource.Manager.Services.Foundations.Applications;
using Coolify.Resource.Manager.Services.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Services.Foundations.Databases;
using Coolify.Resource.Manager.Services.Foundations.Deployments;
using Coolify.Resource.Manager.Services.Foundations.Projects;
using Coolify.Resource.Manager.Services.Foundations.Servers;
using Coolify.Resource.Manager.Services.Foundations.Teams;
using Coolify.Resource.Manager.Services.Processings.Applications;
using Coolify.Resource.Manager.Services.Processings.CoolifyServices;
using Coolify.Resource.Manager.Services.Processings.Databases;
using Coolify.Resource.Manager.Services.Processings.Deployments;
using Coolify.Resource.Manager.Services.Processings.Projects;
using Coolify.Resource.Manager.Services.Processings.Servers;
using Coolify.Resource.Manager.Services.Processings.Teams;
using Microsoft.Extensions.DependencyInjection;

namespace Coolify.Resource.Manager.Extensions
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

            // Processing Services
            services.AddTransient<IServerProcessingService, ServerProcessingService>();
            services.AddTransient<IProjectProcessingService, ProjectProcessingService>();
            services.AddTransient<IApplicationProcessingService, ApplicationProcessingService>();
            services.AddTransient<IDatabaseProcessingService, DatabaseProcessingService>();
            services.AddTransient<ICoolifyServiceProcessingService, CoolifyServiceProcessingService>();
            services.AddTransient<IDeploymentProcessingService, DeploymentProcessingService>();
            services.AddTransient<ITeamProcessingService, TeamProcessingService>();

            // Clients (public surface)
            services.AddTransient<IServerClient, ServerClient>();
            services.AddTransient<IProjectClient, ProjectClient>();
            services.AddTransient<IApplicationClient, ApplicationClient>();
            services.AddTransient<IDatabaseClient, DatabaseClient>();
            services.AddTransient<ICoolifyServiceClient, CoolifyServiceClient>();
            services.AddTransient<IDeploymentClient, DeploymentClient>();
            services.AddTransient<ITeamClient, TeamClient>();

            return services;
        }
    }
}
