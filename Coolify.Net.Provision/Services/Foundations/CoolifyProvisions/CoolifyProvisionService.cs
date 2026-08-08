// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Coolify.Net;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Provision.Brokers.Loggings;
using Coolify.Net.Provision.Models.Configurations;

namespace Coolify.Net.Provision.Services.Foundations.CoolifyProvisions
{
    public class CoolifyProvisionService : ICoolifyProvisionService
    {
        private readonly ICoolifyClient coolifyClient;
        private readonly ILoggingBroker loggingBroker;

        public CoolifyProvisionService(
            ICoolifyClient coolifyClient,
            ILoggingBroker loggingBroker)
        {
            this.coolifyClient = coolifyClient;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Project> ProvisionProjectAsync(
            string projectName)
        {
            this.loggingBroker.LogActivity(message: $"Provisioning project {projectName}...");

            var project = new Project
            {
                Name = projectName,
                Description = $"Provisioned by {nameof(Coolify.Net.Provision)}."
            };

            Project provisionedProject =
                await this.coolifyClient.Projects.AddProjectAsync(project);

            this.loggingBroker.LogActivity(message: $"Project {projectName} provisioned.");

            return provisionedProject;
        }

        public async ValueTask<CoolifyEnvironment> ProvisionEnvironmentAsync(
            string projectName,
            string environment,
            Project project)
        {
            string environmentName = $"{projectName}-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning environment {environmentName}...");

            var coolifyEnvironment = new CoolifyEnvironment
            {
                Name = environmentName
            };

            CoolifyEnvironment provisionedEnvironment =
                await this.coolifyClient.Projects.AddEnvironmentAsync(project.Uuid, coolifyEnvironment);

            this.loggingBroker.LogActivity(message: $"Environment {environmentName} provisioned.");

            return provisionedEnvironment;
        }

        public async ValueTask<PostgreSqlDatabase> ProvisionPostgresDatabaseAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment,
            PostgresConfiguration postgresConfiguration)
        {
            string databaseName = $"{projectName}-postgres-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {databaseName}...");

            var postgresDatabase = new PostgreSqlDatabase
            {
                Name = databaseName,
                ServerUuid = serverUuid,
                ProjectUuid = project.Uuid,
                EnvironmentName = coolifyEnvironment.Name,
                PostgresUser = postgresConfiguration.PostgresUser,
                PostgresPassword = postgresConfiguration.PostgresPassword,
                PostgresDb = postgresConfiguration.PostgresDb
            };

            PostgreSqlDatabase provisionedDatabase =
                await this.coolifyClient.Databases.AddPostgreSqlDatabaseAsync(postgresDatabase);

            await this.coolifyClient.Databases.StartDatabaseAsync(provisionedDatabase.Uuid);

            this.loggingBroker.LogActivity(message: $"{databaseName} provisioned and started.");

            return provisionedDatabase;
        }

        public async ValueTask<RedisDatabase> ProvisionRedisDatabaseAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment)
        {
            string databaseName = $"{projectName}-redis-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {databaseName}...");

            var redisDatabase = new RedisDatabase
            {
                Name = databaseName,
                ServerUuid = serverUuid,
                ProjectUuid = project.Uuid,
                EnvironmentName = coolifyEnvironment.Name
            };

            RedisDatabase provisionedDatabase =
                await this.coolifyClient.Databases.AddRedisDatabaseAsync(redisDatabase);

            await this.coolifyClient.Databases.StartDatabaseAsync(provisionedDatabase.Uuid);

            this.loggingBroker.LogActivity(message: $"{databaseName} provisioned and started.");

            return provisionedDatabase;
        }

        public async ValueTask<Application> ProvisionWebsiteApplicationAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment,
            GitApplicationConfiguration websiteConfiguration)
        {
            string applicationName = $"{projectName}-website-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {applicationName}...");

            var website = new Application
            {
                Name = applicationName,
                ServerUuid = serverUuid,
                ProjectUuid = project.Uuid,
                EnvironmentName = coolifyEnvironment.Name,
                GitRepository = websiteConfiguration.GitRepository,
                GitBranch = websiteConfiguration.GitBranch,
                BuildPack = websiteConfiguration.BuildPack,
                InstantDeploy = true
            };

            Application provisionedWebsite =
                await this.coolifyClient.Applications.AddPublicApplicationAsync(website);

            this.loggingBroker.LogActivity(message: $"{applicationName} provisioned and deploying.");

            return provisionedWebsite;
        }

        public async ValueTask<Application> ProvisionWebApiApplicationAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment,
            GitApplicationConfiguration webApiConfiguration)
        {
            string applicationName = $"{projectName}-webapi-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {applicationName}...");

            var webApi = new Application
            {
                Name = applicationName,
                ServerUuid = serverUuid,
                ProjectUuid = project.Uuid,
                EnvironmentName = coolifyEnvironment.Name,
                GitRepository = webApiConfiguration.GitRepository,
                GitBranch = webApiConfiguration.GitBranch,
                BuildPack = webApiConfiguration.BuildPack,
                InstantDeploy = true
            };

            Application provisionedWebApi =
                await this.coolifyClient.Applications.AddPublicApplicationAsync(webApi);

            this.loggingBroker.LogActivity(message: $"{applicationName} provisioned and deploying.");

            return provisionedWebApi;
        }

        public async ValueTask DeprovisionProjectAsync(
            string projectName,
            Project project)
        {
            this.loggingBroker.LogActivity(message: $"Deprovisioning project {projectName}...");

            await this.coolifyClient.Projects.RemoveProjectAsync(project.Uuid);

            this.loggingBroker.LogActivity(message: $"Project {projectName} deprovisioned.");
        }
    }
}
