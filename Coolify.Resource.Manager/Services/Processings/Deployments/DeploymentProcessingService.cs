// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Services.Foundations.Deployments;

namespace Coolify.Resource.Manager.Services.Processings.Deployments
{
    public partial class DeploymentProcessingService : IDeploymentProcessingService
    {
        private readonly IDeploymentService deploymentService;
        private readonly ILoggingBroker loggingBroker;

        public DeploymentProcessingService(
            IDeploymentService deploymentService,
            ILoggingBroker loggingBroker)
        {
            this.deploymentService = deploymentService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Deployment>> RetrieveAllDeploymentsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.deploymentService.RetrieveAllDeploymentsAsync(cancellationToken);
                });

        public ValueTask<Deployment> RetrieveDeploymentByUuidAsync(
            string deploymentUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDeploymentUuid(deploymentUuid);

                    return await this.deploymentService.RetrieveDeploymentByUuidAsync(deploymentUuid, cancellationToken);
                });

        public ValueTask<Deployment> DeployByUuidAsync(
            string uuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDeploymentUuid(uuid);

                    return await this.deploymentService.DeployByUuidAsync(uuid, cancellationToken);
                });

        public ValueTask<Deployment> CancelDeploymentAsync(
            string deploymentUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDeploymentUuid(deploymentUuid);

                    return await this.deploymentService.CancelDeploymentAsync(deploymentUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<Deployment>> RetrieveApplicationDeploymentsAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    return await this.deploymentService.RetrieveApplicationDeploymentsAsync(
                        applicationUuid, cancellationToken);
                });
    }
}
