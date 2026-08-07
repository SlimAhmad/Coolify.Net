// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Deployments;
using Coolify.Resource.Manager.Models.Foundations.Deployments;

namespace Coolify.Resource.Manager.Services.Foundations.Deployments
{
    public partial class DeploymentService : IDeploymentService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public DeploymentService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Deployment>> RetrieveAllDeploymentsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalDeployment> externalDeployments =
                        await this.coolifyApiBroker.GetAllDeploymentsAsync(cancellationToken);

                    return externalDeployments.Select(ConvertToDeployment);
                });

        public ValueTask<Deployment> RetrieveDeploymentByUuidAsync(
            string deploymentUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDeploymentUuid(deploymentUuid);

                    ExternalDeployment externalDeployment =
                        await this.coolifyApiBroker.GetDeploymentByUuidAsync(deploymentUuid, cancellationToken);

                    return ConvertToDeployment(externalDeployment);
                });

        public ValueTask<Deployment> DeployByUuidAsync(
            string uuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDeploymentUuid(uuid);

                    ExternalDeployment externalDeployment =
                        await this.coolifyApiBroker.PostDeployAsync(uuid, cancellationToken);

                    return ConvertToDeployment(externalDeployment);
                });

        public ValueTask<Deployment> CancelDeploymentAsync(
            string deploymentUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDeploymentUuid(deploymentUuid);

                    ExternalDeployment externalDeployment =
                        await this.coolifyApiBroker.PostDeploymentCancelAsync(deploymentUuid, cancellationToken);

                    return ConvertToDeployment(externalDeployment);
                });

        public ValueTask<IEnumerable<Deployment>> RetrieveApplicationDeploymentsAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    IEnumerable<ExternalDeployment> externalDeployments =
                        await this.coolifyApiBroker.GetApplicationDeploymentsAsync(applicationUuid, cancellationToken);

                    return externalDeployments.Select(ConvertToDeployment);
                });

        // ---- Conversion helpers ----

        private static Deployment ConvertToDeployment(ExternalDeployment externalDeployment) =>
            new Deployment
            {
                Uuid = externalDeployment.Uuid,
                ApplicationUuid = externalDeployment.ApplicationUuid,
                ServerUuid = externalDeployment.ServerUuid,
                Status = externalDeployment.Status,
                Logs = externalDeployment.Logs,
                CommitSha = externalDeployment.CommitSha,
                Branch = externalDeployment.Branch,
                ForceRebuild = externalDeployment.ForceRebuild,
                CreatedAt = externalDeployment.CreatedAt,
                UpdatedAt = externalDeployment.UpdatedAt
            };
    }
}
