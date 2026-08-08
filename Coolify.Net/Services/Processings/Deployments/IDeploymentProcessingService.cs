// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Deployments;

namespace Coolify.Net.Services.Processings.Deployments
{
    public interface IDeploymentProcessingService
    {
        ValueTask<IEnumerable<Deployment>> RetrieveAllDeploymentsAsync(CancellationToken cancellationToken = default);
        ValueTask<Deployment> RetrieveDeploymentByUuidAsync(string deploymentUuid, CancellationToken cancellationToken = default);
        ValueTask<Deployment> DeployByUuidAsync(string uuid, CancellationToken cancellationToken = default);
        ValueTask<Deployment> CancelDeploymentAsync(string deploymentUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Deployment>> RetrieveApplicationDeploymentsAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
