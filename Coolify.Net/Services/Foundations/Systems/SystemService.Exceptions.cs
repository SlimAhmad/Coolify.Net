// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Systems.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Foundations.Systems
{
    public partial class SystemService
    {
        private delegate ValueTask<T> ReturningSystemFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningSystemFunction<T> returningSystemFunction)
        {
            try
            {
                return await returningSystemFunction();
            }
            catch (NullSystemException nullSystemException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullSystemException);
            }
            catch (InvalidSystemException invalidSystemException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidSystemException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidSystemException =
                    new InvalidSystemException(
                        message: "Invalid system.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidSystemException);
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
                var failedSystemDependencyException =
                    new FailedSystemDependencyException(
                        message: "Failed system dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedSystemDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedSystemDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedSystemDependencyException =
                    new FailedSystemDependencyException(
                        message: "Failed system dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedSystemDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutSystemException =
                    new TimeoutSystemException(
                        message: "System dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutSystemException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedSystemServiceException =
                    new FailedSystemServiceException(
                        message: "Failed system service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedSystemServiceException);
            }
        }

        private async ValueTask<SystemValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var systemValidationException = new SystemValidationException(
                message: "System validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(systemValidationException);

            return systemValidationException;
        }

        private async ValueTask<SystemDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var systemDependencyValidationException = new SystemDependencyValidationException(
                message: "System dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(systemDependencyValidationException);

            return systemDependencyValidationException;
        }

        private async ValueTask<SystemDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var systemDependencyException = new SystemDependencyException(
                message: "System dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(systemDependencyException);

            return systemDependencyException;
        }

        private async ValueTask<SystemDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var systemDependencyException = new SystemDependencyException(
                message: "System dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(systemDependencyException);

            return systemDependencyException;
        }

        private async ValueTask<SystemServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var systemServiceException = new SystemServiceException(
                message: "System service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(systemServiceException);

            return systemServiceException;
        }
    }
}
