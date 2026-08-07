// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;

namespace Coolify.Resource.Manager.Services.Foundations.Deployments
{
    public interface IDeploymentService
    {
        ValueTask<IEnumerable<Deployment>> RetrieveAllDeploymentsAsync(CancellationToken cancellationToken = default);
        ValueTask<Deployment> RetrieveDeploymentByUuidAsync(string deploymentUuid, CancellationToken cancellationToken = default);
        ValueTask<Deployment> DeployByUuidAsync(string uuid, CancellationToken cancellationToken = default);
        ValueTask<Deployment> CancelDeploymentAsync(string deploymentUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<Deployment>> RetrieveApplicationDeploymentsAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
