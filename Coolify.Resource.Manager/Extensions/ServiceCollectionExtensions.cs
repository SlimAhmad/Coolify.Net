// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net.Http.Headers;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Clients.Projects;
using Coolify.Resource.Manager.Clients.Servers;
using Coolify.Resource.Manager.Services.Foundations.Projects;
using Coolify.Resource.Manager.Services.Foundations.Servers;
using Coolify.Resource.Manager.Services.Processings.Servers;
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

            // Processing Services
            services.AddTransient<IServerProcessingService, ServerProcessingService>();

            // Clients (public surface)
            services.AddTransient<IServerClient, ServerClient>();
            services.AddTransient<IProjectClient, ProjectClient>();

            return services;
        }
    }
}
