// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions;
using Coolify.Resource.Manager.Models.Processings.Projects.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Processings.Projects
{
    public partial class ProjectProcessingService
    {
        private delegate ValueTask<T> ReturningProjectProcessingFunction<T>();
        private delegate ValueTask ReturningNothingProjectProcessingFunction();

        private async ValueTask<T> TryCatch<T>(ReturningProjectProcessingFunction<T> returningProjectProcessingFunction)
        {
            try
            {
                return await returningProjectProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutProjectProcessingException =
                    new TimeoutProjectProcessingException(
                        message: "Project processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutProjectProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullProjectProcessingException nullProjectProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullProjectProcessingException);
            }
            catch (InvalidProjectProcessingException invalidProjectProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidProjectProcessingException);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(projectValidationException);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(projectDependencyValidationException);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(projectDependencyException);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(projectServiceException);
            }
            catch (Exception exception)
            {
                var failedProjectProcessingServiceException =
                    new ProjectProcessingServiceException(
                        message: "Project processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedProjectProcessingServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingProjectProcessingFunction returningNothingProjectProcessingFunction)
        {
            try
            {
                await returningNothingProjectProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutProjectProcessingException =
                    new TimeoutProjectProcessingException(
                        message: "Project processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutProjectProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullProjectProcessingException nullProjectProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullProjectProcessingException);
            }
            catch (InvalidProjectProcessingException invalidProjectProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidProjectProcessingException);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(projectValidationException);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(projectDependencyValidationException);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(projectDependencyException);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(projectServiceException);
            }
            catch (Exception exception)
            {
                var failedProjectProcessingServiceException =
                    new ProjectProcessingServiceException(
                        message: "Project processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedProjectProcessingServiceException);
            }
        }

        private async ValueTask<ProjectProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var projectProcessingValidationException = new ProjectProcessingValidationException(
                message: "Project processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(projectProcessingValidationException);

            return projectProcessingValidationException;
        }

        private async ValueTask<ProjectProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var projectProcessingDependencyValidationException = new ProjectProcessingDependencyValidationException(
                message: "Project processing dependency validation error occurred, fix the errors and try again.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(projectProcessingDependencyValidationException);

            return projectProcessingDependencyValidationException;
        }

        private async ValueTask<ProjectProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var projectProcessingDependencyException = new ProjectProcessingDependencyException(
                message: "Project processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(projectProcessingDependencyException);

            return projectProcessingDependencyException;
        }

        private async ValueTask<ProjectProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var projectProcessingDependencyException = new ProjectProcessingDependencyException(
                message: "Project processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(projectProcessingDependencyException);

            return projectProcessingDependencyException;
        }

        private async ValueTask<ProjectProcessingServiceException>
            CreateAndLogServiceExceptionAsync(ProjectProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
