// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Foundations.Deployments
{
    public partial class DeploymentService
    {
        private delegate ValueTask<T> ReturningDeploymentFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningDeploymentFunction<T> returningDeploymentFunction)
        {
            try
            {
                return await returningDeploymentFunction();
            }
            catch (NullDeploymentException nullDeploymentException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullDeploymentException);
            }
            catch (InvalidDeploymentException invalidDeploymentException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidDeploymentException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidDeploymentException =
                    new InvalidDeploymentException(
                        message: "Invalid deployment.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidDeploymentException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsDeploymentException =
                    new AlreadyExistsDeploymentException(
                        message: "Deployment already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsDeploymentException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode is
                    HttpStatusCode.Unauthorized or
                    HttpStatusCode.Forbidden or
                    HttpStatusCode.NotFound or
                    HttpStatusCode.TooManyRequests or
                    HttpStatusCode.ServiceUnavailable or
                    HttpStatusCode.InternalServerError)
            {
                var failedDeploymentDependencyException =
                    new FailedDeploymentDependencyException(
                        message: "Failed deployment dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedDeploymentDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedDeploymentDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedDeploymentDependencyException =
                    new FailedDeploymentDependencyException(
                        message: "Failed deployment dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedDeploymentDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutDeploymentException =
                    new TimeoutDeploymentException(
                        message: "Deployment dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutDeploymentException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedDeploymentServiceException =
                    new FailedDeploymentServiceException(
                        message: "Failed deployment service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedDeploymentServiceException);
            }
        }

        private async ValueTask<DeploymentValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var deploymentValidationException = new DeploymentValidationException(
                message: "Deployment validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(deploymentValidationException);

            return deploymentValidationException;
        }

        private async ValueTask<DeploymentDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var deploymentDependencyValidationException = new DeploymentDependencyValidationException(
                message: "Deployment dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(deploymentDependencyValidationException);

            return deploymentDependencyValidationException;
        }

        private async ValueTask<DeploymentDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var deploymentDependencyException = new DeploymentDependencyException(
                message: "Deployment dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(deploymentDependencyException);

            return deploymentDependencyException;
        }

        private async ValueTask<DeploymentDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var deploymentDependencyException = new DeploymentDependencyException(
                message: "Deployment dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(deploymentDependencyException);

            return deploymentDependencyException;
        }

        private async ValueTask<DeploymentServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var deploymentServiceException = new DeploymentServiceException(
                message: "Deployment service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(deploymentServiceException);

            return deploymentServiceException;
        }
    }
}
