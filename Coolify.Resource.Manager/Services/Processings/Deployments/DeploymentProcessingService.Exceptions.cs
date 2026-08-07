// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;
using Coolify.Resource.Manager.Models.Processings.Deployments.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Processings.Deployments
{
    public partial class DeploymentProcessingService
    {
        private delegate ValueTask<T> ReturningDeploymentProcessingFunction<T>();

        private async ValueTask<T> TryCatch<T>(
            ReturningDeploymentProcessingFunction<T> returningDeploymentProcessingFunction)
        {
            try
            {
                return await returningDeploymentProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutDeploymentProcessingException =
                    new TimeoutDeploymentProcessingException(
                        message: "Deployment processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutDeploymentProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullDeploymentProcessingException nullDeploymentProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullDeploymentProcessingException);
            }
            catch (InvalidDeploymentProcessingException invalidDeploymentProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidDeploymentProcessingException);
            }
            catch (DeploymentValidationException deploymentValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(deploymentValidationException);
            }
            catch (DeploymentDependencyValidationException deploymentDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(deploymentDependencyValidationException);
            }
            catch (DeploymentDependencyException deploymentDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(deploymentDependencyException);
            }
            catch (DeploymentServiceException deploymentServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(deploymentServiceException);
            }
            catch (Exception exception)
            {
                var failedDeploymentProcessingServiceException =
                    new DeploymentProcessingServiceException(
                        message: "Deployment processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedDeploymentProcessingServiceException);
            }
        }

        private async ValueTask<DeploymentProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var deploymentProcessingValidationException = new DeploymentProcessingValidationException(
                message: "Deployment processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(deploymentProcessingValidationException);

            return deploymentProcessingValidationException;
        }

        private async ValueTask<DeploymentProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var deploymentProcessingDependencyValidationException =
                new DeploymentProcessingDependencyValidationException(
                    message: "Deployment processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(deploymentProcessingDependencyValidationException);

            return deploymentProcessingDependencyValidationException;
        }

        private async ValueTask<DeploymentProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var deploymentProcessingDependencyException = new DeploymentProcessingDependencyException(
                message: "Deployment processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(deploymentProcessingDependencyException);

            return deploymentProcessingDependencyException;
        }

        private async ValueTask<DeploymentProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var deploymentProcessingDependencyException = new DeploymentProcessingDependencyException(
                message: "Deployment processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(deploymentProcessingDependencyException);

            return deploymentProcessingDependencyException;
        }

        private async ValueTask<DeploymentProcessingServiceException>
            CreateAndLogServiceExceptionAsync(DeploymentProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
