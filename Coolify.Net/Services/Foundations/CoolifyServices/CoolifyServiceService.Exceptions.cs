// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceService
    {
        private delegate ValueTask<T> ReturningCoolifyServiceFunction<T>();
        private delegate ValueTask ReturningNothingCoolifyServiceFunction();

        private async ValueTask<T> TryCatch<T>(ReturningCoolifyServiceFunction<T> returningCoolifyServiceFunction)
        {
            try
            {
                return await returningCoolifyServiceFunction();
            }
            catch (NullCoolifyServiceException nullCoolifyServiceException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullCoolifyServiceException);
            }
            catch (InvalidCoolifyServiceException invalidCoolifyServiceException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidCoolifyServiceException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidCoolifyServiceException =
                    new InvalidCoolifyServiceException(
                        message: "Invalid service.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidCoolifyServiceException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsCoolifyServiceException =
                    new AlreadyExistsCoolifyServiceException(
                        message: "Service already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsCoolifyServiceException);
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
                var failedCoolifyServiceDependencyException =
                    new FailedCoolifyServiceDependencyException(
                        message: "Failed service dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedCoolifyServiceDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedCoolifyServiceDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCoolifyServiceDependencyException =
                    new FailedCoolifyServiceDependencyException(
                        message: "Failed service dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedCoolifyServiceDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutCoolifyServiceException =
                    new TimeoutCoolifyServiceException(
                        message: "Service dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutCoolifyServiceException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedCoolifyServiceServiceException =
                    new FailedCoolifyServiceServiceException(
                        message: "Failed service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedCoolifyServiceServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingCoolifyServiceFunction returningNothingCoolifyServiceFunction)
        {
            try
            {
                await returningNothingCoolifyServiceFunction();
            }
            catch (NullCoolifyServiceException nullCoolifyServiceException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullCoolifyServiceException);
            }
            catch (InvalidCoolifyServiceException invalidCoolifyServiceException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidCoolifyServiceException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedCoolifyServiceDependencyException =
                    new FailedCoolifyServiceDependencyException(
                        message: "Failed service dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyExceptionAsync(failedCoolifyServiceDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutCoolifyServiceException =
                    new TimeoutCoolifyServiceException(
                        message: "Service dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutCoolifyServiceException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedCoolifyServiceServiceException =
                    new FailedCoolifyServiceServiceException(
                        message: "Failed service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedCoolifyServiceServiceException);
            }
        }

        private async ValueTask<CoolifyServiceValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var coolifyServiceValidationException = new CoolifyServiceValidationException(
                message: "Service validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(coolifyServiceValidationException);

            return coolifyServiceValidationException;
        }

        private async ValueTask<CoolifyServiceDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var coolifyServiceDependencyValidationException = new CoolifyServiceDependencyValidationException(
                message: "Service dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(coolifyServiceDependencyValidationException);

            return coolifyServiceDependencyValidationException;
        }

        private async ValueTask<CoolifyServiceDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var coolifyServiceDependencyException = new CoolifyServiceDependencyException(
                message: "Service dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(coolifyServiceDependencyException);

            return coolifyServiceDependencyException;
        }

        private async ValueTask<CoolifyServiceDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var coolifyServiceDependencyException = new CoolifyServiceDependencyException(
                message: "Service dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(coolifyServiceDependencyException);

            return coolifyServiceDependencyException;
        }

        private async ValueTask<CoolifyServiceServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var coolifyServiceServiceException = new CoolifyServiceServiceException(
                message: "Service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(coolifyServiceServiceException);

            return coolifyServiceServiceException;
        }
    }
}
