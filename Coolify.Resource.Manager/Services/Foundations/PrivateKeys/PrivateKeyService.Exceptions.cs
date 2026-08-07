// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyService
    {
        private delegate ValueTask<T> ReturningPrivateKeyFunction<T>();
        private delegate ValueTask ReturningNothingPrivateKeyFunction();

        private async ValueTask<T> TryCatch<T>(ReturningPrivateKeyFunction<T> returningPrivateKeyFunction)
        {
            try
            {
                return await returningPrivateKeyFunction();
            }
            catch (NullPrivateKeyException nullPrivateKeyException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullPrivateKeyException);
            }
            catch (InvalidPrivateKeyException invalidPrivateKeyException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidPrivateKeyException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidPrivateKeyException =
                    new InvalidPrivateKeyException(
                        message: "Invalid private key.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidPrivateKeyException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsPrivateKeyException =
                    new AlreadyExistsPrivateKeyException(
                        message: "Private key already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsPrivateKeyException);
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
                var failedPrivateKeyDependencyException =
                    new FailedPrivateKeyDependencyException(
                        message: "Failed private key dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedPrivateKeyDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedPrivateKeyDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedPrivateKeyDependencyException =
                    new FailedPrivateKeyDependencyException(
                        message: "Failed private key dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedPrivateKeyDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutPrivateKeyException =
                    new TimeoutPrivateKeyException(
                        message: "Private key dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutPrivateKeyException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedPrivateKeyServiceException =
                    new FailedPrivateKeyServiceException(
                        message: "Failed private key service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedPrivateKeyServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingPrivateKeyFunction returningNothingPrivateKeyFunction)
        {
            try
            {
                await returningNothingPrivateKeyFunction();
            }
            catch (NullPrivateKeyException nullPrivateKeyException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullPrivateKeyException);
            }
            catch (InvalidPrivateKeyException invalidPrivateKeyException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidPrivateKeyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedPrivateKeyDependencyException =
                    new FailedPrivateKeyDependencyException(
                        message: "Failed private key dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyExceptionAsync(failedPrivateKeyDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutPrivateKeyException =
                    new TimeoutPrivateKeyException(
                        message: "Private key dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutPrivateKeyException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedPrivateKeyServiceException =
                    new FailedPrivateKeyServiceException(
                        message: "Failed private key service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedPrivateKeyServiceException);
            }
        }

        private async ValueTask<PrivateKeyValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var privateKeyValidationException = new PrivateKeyValidationException(
                message: "Private key validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(privateKeyValidationException);

            return privateKeyValidationException;
        }

        private async ValueTask<PrivateKeyDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var privateKeyDependencyValidationException = new PrivateKeyDependencyValidationException(
                message: "Private key dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(privateKeyDependencyValidationException);

            return privateKeyDependencyValidationException;
        }

        private async ValueTask<PrivateKeyDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var privateKeyDependencyException = new PrivateKeyDependencyException(
                message: "Private key dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(privateKeyDependencyException);

            return privateKeyDependencyException;
        }

        private async ValueTask<PrivateKeyDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var privateKeyDependencyException = new PrivateKeyDependencyException(
                message: "Private key dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(privateKeyDependencyException);

            return privateKeyDependencyException;
        }

        private async ValueTask<PrivateKeyServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var privateKeyServiceException = new PrivateKeyServiceException(
                message: "Private key service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(privateKeyServiceException);

            return privateKeyServiceException;
        }
    }
}
