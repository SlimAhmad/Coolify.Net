// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Coolify.Net.Models.Processings.CoolifyServices.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Processings.CoolifyServices
{
    public partial class CoolifyServiceProcessingService
    {
        private delegate ValueTask<T> ReturningCoolifyServiceProcessingFunction<T>();
        private delegate ValueTask ReturningNothingCoolifyServiceProcessingFunction();

        private async ValueTask<T> TryCatch<T>(
            ReturningCoolifyServiceProcessingFunction<T> returningCoolifyServiceProcessingFunction)
        {
            try
            {
                return await returningCoolifyServiceProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutCoolifyServiceProcessingException =
                    new TimeoutCoolifyServiceProcessingException(
                        message: "Service processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutCoolifyServiceProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullCoolifyServiceProcessingException nullCoolifyServiceProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullCoolifyServiceProcessingException);
            }
            catch (InvalidCoolifyServiceProcessingException invalidCoolifyServiceProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidCoolifyServiceProcessingException);
            }
            catch (CoolifyServiceValidationException coolifyServiceValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(coolifyServiceValidationException);
            }
            catch (CoolifyServiceDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(coolifyServiceDependencyValidationException);
            }
            catch (CoolifyServiceDependencyException coolifyServiceDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(coolifyServiceDependencyException);
            }
            catch (CoolifyServiceServiceException coolifyServiceServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(coolifyServiceServiceException);
            }
            catch (Exception exception)
            {
                var failedCoolifyServiceProcessingServiceException =
                    new CoolifyServiceProcessingServiceException(
                        message: "Service processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedCoolifyServiceProcessingServiceException);
            }
        }

        private async ValueTask TryCatch(
            ReturningNothingCoolifyServiceProcessingFunction returningNothingCoolifyServiceProcessingFunction)
        {
            try
            {
                await returningNothingCoolifyServiceProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutCoolifyServiceProcessingException =
                    new TimeoutCoolifyServiceProcessingException(
                        message: "Service processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutCoolifyServiceProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullCoolifyServiceProcessingException nullCoolifyServiceProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullCoolifyServiceProcessingException);
            }
            catch (InvalidCoolifyServiceProcessingException invalidCoolifyServiceProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidCoolifyServiceProcessingException);
            }
            catch (CoolifyServiceValidationException coolifyServiceValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(coolifyServiceValidationException);
            }
            catch (CoolifyServiceDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(coolifyServiceDependencyValidationException);
            }
            catch (CoolifyServiceDependencyException coolifyServiceDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(coolifyServiceDependencyException);
            }
            catch (CoolifyServiceServiceException coolifyServiceServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(coolifyServiceServiceException);
            }
            catch (Exception exception)
            {
                var failedCoolifyServiceProcessingServiceException =
                    new CoolifyServiceProcessingServiceException(
                        message: "Service processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedCoolifyServiceProcessingServiceException);
            }
        }

        private async ValueTask<CoolifyServiceProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var coolifyServiceProcessingValidationException = new CoolifyServiceProcessingValidationException(
                message: "Service processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(coolifyServiceProcessingValidationException);

            return coolifyServiceProcessingValidationException;
        }

        private async ValueTask<CoolifyServiceProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var coolifyServiceProcessingDependencyValidationException =
                new CoolifyServiceProcessingDependencyValidationException(
                    message: "Service processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(coolifyServiceProcessingDependencyValidationException);

            return coolifyServiceProcessingDependencyValidationException;
        }

        private async ValueTask<CoolifyServiceProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var coolifyServiceProcessingDependencyException = new CoolifyServiceProcessingDependencyException(
                message: "Service processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(coolifyServiceProcessingDependencyException);

            return coolifyServiceProcessingDependencyException;
        }

        private async ValueTask<CoolifyServiceProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var coolifyServiceProcessingDependencyException = new CoolifyServiceProcessingDependencyException(
                message: "Service processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(coolifyServiceProcessingDependencyException);

            return coolifyServiceProcessingDependencyException;
        }

        private async ValueTask<CoolifyServiceProcessingServiceException>
            CreateAndLogServiceExceptionAsync(CoolifyServiceProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
