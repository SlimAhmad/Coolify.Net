// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Provision.Models.Configurations;

namespace Coolify.Net.Provision.Services.Foundations.CoolifyProvisions
{
    public interface ICoolifyProvisionService
    {
        ValueTask<Project> ProvisionProjectAsync(
            string projectName);

        ValueTask<CoolifyEnvironment> ProvisionEnvironmentAsync(
            string projectName,
            string environment,
            Project project);

        ValueTask<PostgreSqlDatabase> ProvisionPostgresDatabaseAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment,
            PostgresConfiguration postgresConfiguration);

        ValueTask<RedisDatabase> ProvisionRedisDatabaseAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment);

        ValueTask<Application> ProvisionWebsiteApplicationAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment,
            GitApplicationConfiguration websiteConfiguration);

        ValueTask<Application> ProvisionWebApiApplicationAsync(
            string projectName,
            string environment,
            string serverUuid,
            Project project,
            CoolifyEnvironment coolifyEnvironment,
            GitApplicationConfiguration webApiConfiguration);

        ValueTask DeprovisionProjectAsync(
            string projectName,
            Project project);
    }
}
