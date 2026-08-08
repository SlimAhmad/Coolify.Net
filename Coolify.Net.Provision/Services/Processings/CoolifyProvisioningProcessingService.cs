// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Provision.Brokers.Configurations;
using Coolify.Net.Provision.Models.Configurations;
using Coolify.Net.Provision.Services.Foundations.CoolifyProvisions;


namespace Coolify.Net.Provision.Services.Processings
{
    public class CoolifyProvisioningProcessingService : ICoolifyProvisioningProcessingService
    {
        private readonly ICoolifyProvisionService coolifyProvisionService;
        private readonly IConfigurationBroker configurationBroker;

        public CoolifyProvisioningProcessingService(
            ICoolifyProvisionService coolifyProvisionService,
            IConfigurationBroker configurationBroker)
        {
            this.coolifyProvisionService = coolifyProvisionService;
            this.configurationBroker = configurationBroker;
        }

        public async ValueTask ProcessAsync()
        {
            CoolifyProvisionConfiguration configuration =
                this.configurationBroker.GetConfigurations();

            await ProvisionAsync(configuration, configuration.Up);
            await DeprovisionAsync(configuration, configuration.Down);
        }

        private async ValueTask ProvisionAsync(
            CoolifyProvisionConfiguration configuration,
            ProvisionAction provisionAction)
        {
            List<string> environments = RetrieveEnvironments(provisionAction);

            foreach (string environment in environments)
            {
                Project project = await this.coolifyProvisionService
                    .ProvisionProjectAsync(configuration.ProjectName);

                CoolifyEnvironment coolifyEnvironment = await this.coolifyProvisionService
                    .ProvisionEnvironmentAsync(
                        configuration.ProjectName,
                        environment,
                        project);

                PostgreSqlDatabase postgres = await this.coolifyProvisionService
                    .ProvisionPostgresDatabaseAsync(
                        configuration.ProjectName,
                        environment,
                        configuration.ServerUuid,
                        project,
                        coolifyEnvironment,
                        configuration.Postgres);

                RedisDatabase redis = await this.coolifyProvisionService
                    .ProvisionRedisDatabaseAsync(
                        configuration.ProjectName,
                        environment,
                        configuration.ServerUuid,
                        project,
                        coolifyEnvironment);

                Application website = await this.coolifyProvisionService
                    .ProvisionWebsiteApplicationAsync(
                        configuration.ProjectName,
                        environment,
                        configuration.ServerUuid,
                        project,
                        coolifyEnvironment,
                        configuration.Website);

                Application webApi = await this.coolifyProvisionService
                    .ProvisionWebApiApplicationAsync(
                        configuration.ProjectName,
                        environment,
                        configuration.ServerUuid,
                        project,
                        coolifyEnvironment,
                        configuration.WebApi);
            }
        }

        private async ValueTask DeprovisionAsync(
            CoolifyProvisionConfiguration configuration,
            ProvisionAction provisionAction)
        {
            List<string> environments = RetrieveEnvironments(provisionAction);

            foreach (string environment in environments)
            {
                Project project = await this.coolifyProvisionService
                    .ProvisionProjectAsync(configuration.ProjectName);

                await this.coolifyProvisionService.DeprovisionProjectAsync(
                    configuration.ProjectName,
                    project);
            }
        }

        private static List<string> RetrieveEnvironments(ProvisionAction provisionAction) =>
            provisionAction?.Environments ?? new List<string>();
    }
}
