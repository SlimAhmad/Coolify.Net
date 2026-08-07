// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions;
using Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingService
    {
        private delegate ValueTask<T> ReturningPrivateKeyProcessingFunction<T>();
        private delegate ValueTask ReturningNothingPrivateKeyProcessingFunction();

        private async ValueTask<T> TryCatch<T>(
            ReturningPrivateKeyProcessingFunction<T> returningPrivateKeyProcessingFunction)
        {
            try
            {
                return await returningPrivateKeyProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutPrivateKeyProcessingException =
                    new TimeoutPrivateKeyProcessingException(
                        message: "Private key processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutPrivateKeyProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullPrivateKeyProcessingException nullPrivateKeyProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullPrivateKeyProcessingException);
            }
            catch (InvalidPrivateKeyProcessingException invalidPrivateKeyProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidPrivateKeyProcessingException);
            }
            catch (PrivateKeyValidationException privateKeyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(privateKeyValidationException);
            }
            catch (PrivateKeyDependencyValidationException privateKeyDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(privateKeyDependencyValidationException);
            }
            catch (PrivateKeyDependencyException privateKeyDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(privateKeyDependencyException);
            }
            catch (PrivateKeyServiceException privateKeyServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(privateKeyServiceException);
            }
            catch (Exception exception)
            {
                var failedPrivateKeyProcessingServiceException =
                    new PrivateKeyProcessingServiceException(
                        message: "Private key processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedPrivateKeyProcessingServiceException);
            }
        }

        private async ValueTask TryCatch(
            ReturningNothingPrivateKeyProcessingFunction returningNothingPrivateKeyProcessingFunction)
        {
            try
            {
                await returningNothingPrivateKeyProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutPrivateKeyProcessingException =
                    new TimeoutPrivateKeyProcessingException(
                        message: "Private key processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutPrivateKeyProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullPrivateKeyProcessingException nullPrivateKeyProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullPrivateKeyProcessingException);
            }
            catch (InvalidPrivateKeyProcessingException invalidPrivateKeyProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidPrivateKeyProcessingException);
            }
            catch (PrivateKeyValidationException privateKeyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(privateKeyValidationException);
            }
            catch (PrivateKeyDependencyValidationException privateKeyDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(privateKeyDependencyValidationException);
            }
            catch (PrivateKeyDependencyException privateKeyDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(privateKeyDependencyException);
            }
            catch (PrivateKeyServiceException privateKeyServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(privateKeyServiceException);
            }
            catch (Exception exception)
            {
                var failedPrivateKeyProcessingServiceException =
                    new PrivateKeyProcessingServiceException(
                        message: "Private key processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedPrivateKeyProcessingServiceException);
            }
        }

        private async ValueTask<PrivateKeyProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var privateKeyProcessingValidationException = new PrivateKeyProcessingValidationException(
                message: "Private key processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(privateKeyProcessingValidationException);

            return privateKeyProcessingValidationException;
        }

        private async ValueTask<PrivateKeyProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var privateKeyProcessingDependencyValidationException =
                new PrivateKeyProcessingDependencyValidationException(
                    message: "Private key processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(privateKeyProcessingDependencyValidationException);

            return privateKeyProcessingDependencyValidationException;
        }

        private async ValueTask<PrivateKeyProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var privateKeyProcessingDependencyException = new PrivateKeyProcessingDependencyException(
                message: "Private key processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(privateKeyProcessingDependencyException);

            return privateKeyProcessingDependencyException;
        }

        private async ValueTask<PrivateKeyProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var privateKeyProcessingDependencyException = new PrivateKeyProcessingDependencyException(
                message: "Private key processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(privateKeyProcessingDependencyException);

            return privateKeyProcessingDependencyException;
        }

        private async ValueTask<PrivateKeyProcessingServiceException>
            CreateAndLogServiceExceptionAsync(PrivateKeyProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
