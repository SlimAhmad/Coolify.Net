// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Deployments.Exceptions;
using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Models.Processings.Deployments.Exceptions;
using Coolify.Net.Services.Processings.Deployments;
using Xeptions;

namespace Coolify.Net.Clients.Deployments
{
    /// <summary>Provides deployment triggering and management operations.</summary>
    internal class DeploymentClient : IDeploymentClient
    {
        private readonly IDeploymentProcessingService deploymentProcessingService;

        public DeploymentClient(IDeploymentProcessingService deploymentProcessingService) =>
            this.deploymentProcessingService = deploymentProcessingService;

        public async ValueTask<IEnumerable<Deployment>> RetrieveAllDeploymentsAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.deploymentProcessingService.RetrieveAllDeploymentsAsync(cancellationToken);
            }
            catch (DeploymentProcessingValidationException deploymentValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyValidationException deploymentDependencyValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentDependencyValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyException deploymentDependencyException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentDependencyException.InnerException as Xeption);
            }
            catch (DeploymentProcessingServiceException deploymentServiceException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDeploymentClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Deployment> RetrieveDeploymentByUuidAsync(
            string deploymentUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.deploymentProcessingService.RetrieveDeploymentByUuidAsync(
                    deploymentUuid, cancellationToken);
            }
            catch (DeploymentProcessingValidationException deploymentValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyValidationException deploymentDependencyValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentDependencyValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyException deploymentDependencyException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentDependencyException.InnerException as Xeption);
            }
            catch (DeploymentProcessingServiceException deploymentServiceException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDeploymentClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Deployment> DeployByUuidAsync(
            string uuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.deploymentProcessingService.DeployByUuidAsync(uuid, cancellationToken);
            }
            catch (DeploymentProcessingValidationException deploymentValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyValidationException deploymentDependencyValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentDependencyValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyException deploymentDependencyException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentDependencyException.InnerException as Xeption);
            }
            catch (DeploymentProcessingServiceException deploymentServiceException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDeploymentClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Deployment> CancelDeploymentAsync(
            string deploymentUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.deploymentProcessingService.CancelDeploymentAsync(deploymentUuid, cancellationToken);
            }
            catch (DeploymentProcessingValidationException deploymentValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyValidationException deploymentDependencyValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentDependencyValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyException deploymentDependencyException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentDependencyException.InnerException as Xeption);
            }
            catch (DeploymentProcessingServiceException deploymentServiceException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDeploymentClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<Deployment>> RetrieveApplicationDeploymentsAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.deploymentProcessingService.RetrieveApplicationDeploymentsAsync(
                    applicationUuid, cancellationToken);
            }
            catch (DeploymentProcessingValidationException deploymentValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyValidationException deploymentDependencyValidationException)
            {
                throw CreateDeploymentClientValidationException(
                    deploymentDependencyValidationException.InnerException as Xeption);
            }
            catch (DeploymentProcessingDependencyException deploymentDependencyException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentDependencyException.InnerException as Xeption);
            }
            catch (DeploymentProcessingServiceException deploymentServiceException)
            {
                throw CreateDeploymentClientDependencyException(
                    deploymentServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDeploymentClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static DeploymentClientValidationException
            CreateDeploymentClientValidationException(Xeption innerException)
        {
            return new DeploymentClientValidationException(
                message: "Deployment client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static DeploymentClientDependencyException
            CreateDeploymentClientDependencyException(Xeption innerException)
        {
            return new DeploymentClientDependencyException(
                message: "Deployment client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static DeploymentClientServiceException
            CreateDeploymentClientServiceException(Xeption innerException)
        {
            return new DeploymentClientServiceException(
                message: "Deployment client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
