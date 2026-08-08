// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Deployments;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string DeploymentsRelativeUrl = "deployments";

        public async ValueTask<IEnumerable<ExternalDeployment>> GetAllDeploymentsAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalDeployment>>(DeploymentsRelativeUrl, cancellationToken);

        public async ValueTask<ExternalDeployment> GetDeploymentByUuidAsync(
            string deploymentUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalDeployment>($"{DeploymentsRelativeUrl}/{deploymentUuid}", cancellationToken);

        public async ValueTask<ExternalDeployment> PostDeployAsync(
            string uuid, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalDeployment>($"deploy?uuid={uuid}", cancellationToken);

        public async ValueTask<ExternalDeployment> PostDeploymentCancelAsync(
            string deploymentUuid, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalDeployment>(
                    $"{DeploymentsRelativeUrl}/{deploymentUuid}/cancel", cancellationToken);

        public async ValueTask<IEnumerable<ExternalDeployment>> GetApplicationDeploymentsAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalDeployment>>(
                    $"{DeploymentsRelativeUrl}/applications/{applicationUuid}", cancellationToken);
    }
}
