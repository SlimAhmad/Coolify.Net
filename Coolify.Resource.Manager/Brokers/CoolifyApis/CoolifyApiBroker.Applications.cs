// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Applications;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string ApplicationsRelativeUrl = "applications";

        public async ValueTask<IEnumerable<ExternalApplication>> GetAllApplicationsAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalApplication>>(ApplicationsRelativeUrl, cancellationToken);

        public async ValueTask<ExternalApplication> GetApplicationByUuidAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalApplication>($"{ApplicationsRelativeUrl}/{applicationUuid}", cancellationToken);

        public async ValueTask<ExternalApplication> PostPublicApplicationAsync(
            ExternalApplication application, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalApplication>(
                    $"{ApplicationsRelativeUrl}/public", application, cancellationToken);

        public async ValueTask<ExternalApplication> PostPrivateGithubAppApplicationAsync(
            ExternalApplication application, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalApplication>(
                    $"{ApplicationsRelativeUrl}/private-github-app", application, cancellationToken);

        public async ValueTask<ExternalApplication> PostPrivateDeployKeyApplicationAsync(
            ExternalApplication application, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalApplication>(
                    $"{ApplicationsRelativeUrl}/private-deploy-key", application, cancellationToken);

        public async ValueTask<ExternalApplication> PostDockerfileApplicationAsync(
            ExternalApplication application, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalApplication>(
                    $"{ApplicationsRelativeUrl}/dockerfile", application, cancellationToken);

        public async ValueTask<ExternalApplication> PostDockerImageApplicationAsync(
            ExternalApplication application, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalApplication>(
                    $"{ApplicationsRelativeUrl}/dockerimage", application, cancellationToken);

        public async ValueTask<ExternalApplication> PatchApplicationAsync(
            ExternalApplication application, CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalApplication>(
                    $"{ApplicationsRelativeUrl}/{application.Uuid}", application, cancellationToken);

        public async ValueTask DeleteApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync($"{ApplicationsRelativeUrl}/{applicationUuid}", cancellationToken);

        public async ValueTask<IEnumerable<ExternalEnvironmentVariable>> GetApplicationEnvVarsAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalEnvironmentVariable>>(
                    $"{ApplicationsRelativeUrl}/{applicationUuid}/envs", cancellationToken);

        public async ValueTask<ExternalEnvironmentVariable> PostApplicationEnvVarAsync(
            string applicationUuid,
            ExternalEnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalEnvironmentVariable>(
                    $"{ApplicationsRelativeUrl}/{applicationUuid}/envs", environmentVariable, cancellationToken);

        public async ValueTask<ExternalEnvironmentVariable> PatchApplicationEnvVarAsync(
            string applicationUuid,
            ExternalEnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalEnvironmentVariable>(
                    $"{ApplicationsRelativeUrl}/{applicationUuid}/envs", environmentVariable, cancellationToken);

        public async ValueTask<IEnumerable<ExternalEnvironmentVariable>> PatchApplicationEnvVarsBulkAsync(
            string applicationUuid,
            IEnumerable<ExternalEnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default) =>
                await PatchAsync<IEnumerable<ExternalEnvironmentVariable>>(
                    $"{ApplicationsRelativeUrl}/{applicationUuid}/envs/bulk", environmentVariables, cancellationToken);

        public async ValueTask DeleteApplicationEnvVarAsync(
            string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync(
                    $"{ApplicationsRelativeUrl}/{applicationUuid}/envs/{environmentVariableUuid}", cancellationToken);

        public async ValueTask PostApplicationStartAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{ApplicationsRelativeUrl}/{applicationUuid}/start", cancellationToken);

        public async ValueTask PostApplicationStopAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{ApplicationsRelativeUrl}/{applicationUuid}/stop", cancellationToken);

        public async ValueTask PostApplicationRestartAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{ApplicationsRelativeUrl}/{applicationUuid}/restart", cancellationToken);
    }
}
