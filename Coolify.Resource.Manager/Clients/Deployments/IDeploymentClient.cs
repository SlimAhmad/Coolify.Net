// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;

namespace Coolify.Resource.Manager.Clients.Deployments
{
    /// <summary>Defines the contract for managing Coolify deployments.</summary>
    public interface IDeploymentClient
    {
        /// <summary>Retrieves all currently running deployments.</summary>
        /// <exception cref="Exceptions.DeploymentClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.DeploymentClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.DeploymentClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<Deployment>> RetrieveAllDeploymentsAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a deployment by its UUID.</summary>
        ValueTask<Deployment> RetrieveDeploymentByUuidAsync(string deploymentUuid, CancellationToken cancellationToken = default);

        /// <summary>Triggers a deployment by resource UUID.</summary>
        ValueTask<Deployment> DeployByUuidAsync(string uuid, CancellationToken cancellationToken = default);

        /// <summary>Cancels a running deployment.</summary>
        ValueTask<Deployment> CancelDeploymentAsync(string deploymentUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists an application's deployment history.</summary>
        ValueTask<IEnumerable<Deployment>> RetrieveApplicationDeploymentsAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
