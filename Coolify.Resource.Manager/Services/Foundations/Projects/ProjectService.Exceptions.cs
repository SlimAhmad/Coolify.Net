// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Foundations.Projects
{
    public partial class ProjectService
    {
        private delegate ValueTask<T> ReturningProjectFunction<T>();
        private delegate ValueTask ReturningNothingProjectFunction();

        private async ValueTask<T> TryCatch<T>(ReturningProjectFunction<T> returningProjectFunction)
        {
            try
            {
                return await returningProjectFunction();
            }
            catch (NullProjectException nullProjectException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullProjectException);
            }
            catch (InvalidProjectException invalidProjectException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidProjectException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidProjectException =
                    new InvalidProjectException(
                        message: "Invalid project.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidProjectException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsProjectException =
                    new AlreadyExistsProjectException(
                        message: "Project already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsProjectException);
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
                var failedProjectDependencyException =
                    new FailedProjectDependencyException(
                        message: "Failed project dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedProjectDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedProjectDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedProjectDependencyException =
                    new FailedProjectDependencyException(
                        message: "Failed project dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedProjectDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var failedProjectDependencyException =
                    new FailedProjectDependencyException(
                        message: "Failed project dependency error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(failedProjectDependencyException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedProjectServiceException =
                    new FailedProjectServiceException(
                        message: "Failed project service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedProjectServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingProjectFunction returningNothingProjectFunction)
        {
            try
            {
                await returningNothingProjectFunction();
            }
            catch (NullProjectException nullProjectException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullProjectException);
            }
            catch (InvalidProjectException invalidProjectException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidProjectException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedProjectDependencyException =
                    new FailedProjectDependencyException(
                        message: "Failed project dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyExceptionAsync(failedProjectDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var failedProjectDependencyException =
                    new FailedProjectDependencyException(
                        message: "Failed project dependency error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(failedProjectDependencyException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedProjectServiceException =
                    new FailedProjectServiceException(
                        message: "Failed project service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedProjectServiceException);
            }
        }

        private async ValueTask<ProjectValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var projectValidationException = new ProjectValidationException(
                message: "Project validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(projectValidationException);

            return projectValidationException;
        }

        private async ValueTask<ProjectDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var projectDependencyValidationException = new ProjectDependencyValidationException(
                message: "Project dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(projectDependencyValidationException);

            return projectDependencyValidationException;
        }

        private async ValueTask<ProjectDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var projectDependencyException = new ProjectDependencyException(
                message: "Project dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(projectDependencyException);

            return projectDependencyException;
        }

        private async ValueTask<ProjectDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var projectDependencyException = new ProjectDependencyException(
                message: "Project dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(projectDependencyException);

            return projectDependencyException;
        }

        private async ValueTask<ProjectServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var projectServiceException = new ProjectServiceException(
                message: "Project service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(projectServiceException);

            return projectServiceException;
        }
    }
}
