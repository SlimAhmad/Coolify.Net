// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Foundations.Applications
{
    public partial class ApplicationService
    {
        private delegate ValueTask<T> ReturningApplicationFunction<T>();
        private delegate ValueTask ReturningNothingApplicationFunction();

        private async ValueTask<T> TryCatch<T>(ReturningApplicationFunction<T> returningApplicationFunction)
        {
            try
            {
                return await returningApplicationFunction();
            }
            catch (NullApplicationException nullApplicationException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullApplicationException);
            }
            catch (InvalidApplicationException invalidApplicationException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidApplicationException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidApplicationException =
                    new InvalidApplicationException(
                        message: "Invalid application.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidApplicationException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsApplicationException =
                    new AlreadyExistsApplicationException(
                        message: "Application already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsApplicationException);
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
                var failedApplicationDependencyException =
                    new FailedApplicationDependencyException(
                        message: "Failed application dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedApplicationDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedApplicationDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedApplicationDependencyException =
                    new FailedApplicationDependencyException(
                        message: "Failed application dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedApplicationDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutApplicationException =
                    new TimeoutApplicationException(
                        message: "Application dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutApplicationException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedApplicationServiceException =
                    new FailedApplicationServiceException(
                        message: "Failed application service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedApplicationServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingApplicationFunction returningNothingApplicationFunction)
        {
            try
            {
                await returningNothingApplicationFunction();
            }
            catch (NullApplicationException nullApplicationException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullApplicationException);
            }
            catch (InvalidApplicationException invalidApplicationException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidApplicationException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedApplicationDependencyException =
                    new FailedApplicationDependencyException(
                        message: "Failed application dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyExceptionAsync(failedApplicationDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutApplicationException =
                    new TimeoutApplicationException(
                        message: "Application dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutApplicationException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedApplicationServiceException =
                    new FailedApplicationServiceException(
                        message: "Failed application service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedApplicationServiceException);
            }
        }

        private async ValueTask<ApplicationValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var applicationValidationException = new ApplicationValidationException(
                message: "Application validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(applicationValidationException);

            return applicationValidationException;
        }

        private async ValueTask<ApplicationDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var applicationDependencyValidationException = new ApplicationDependencyValidationException(
                message: "Application dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(applicationDependencyValidationException);

            return applicationDependencyValidationException;
        }

        private async ValueTask<ApplicationDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var applicationDependencyException = new ApplicationDependencyException(
                message: "Application dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(applicationDependencyException);

            return applicationDependencyException;
        }

        private async ValueTask<ApplicationDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var applicationDependencyException = new ApplicationDependencyException(
                message: "Application dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(applicationDependencyException);

            return applicationDependencyException;
        }

        private async ValueTask<ApplicationServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var applicationServiceException = new ApplicationServiceException(
                message: "Application service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(applicationServiceException);

            return applicationServiceException;
        }
    }
}
