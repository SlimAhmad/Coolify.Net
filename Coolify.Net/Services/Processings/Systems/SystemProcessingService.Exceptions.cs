// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems.Exceptions;
using Coolify.Net.Models.Processings.Systems.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Processings.Systems
{
    public partial class SystemProcessingService
    {
        private delegate ValueTask<T> ReturningSystemProcessingFunction<T>();

        private async ValueTask<T> TryCatch<T>(
            ReturningSystemProcessingFunction<T> returningSystemProcessingFunction)
        {
            try
            {
                return await returningSystemProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutSystemProcessingException =
                    new TimeoutSystemProcessingException(
                        message: "System processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutSystemProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullSystemProcessingException nullSystemProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullSystemProcessingException);
            }
            catch (InvalidSystemProcessingException invalidSystemProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidSystemProcessingException);
            }
            catch (SystemValidationException systemValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(systemValidationException);
            }
            catch (SystemDependencyValidationException systemDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(systemDependencyValidationException);
            }
            catch (SystemDependencyException systemDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(systemDependencyException);
            }
            catch (SystemServiceException systemServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(systemServiceException);
            }
            catch (Exception exception)
            {
                var failedSystemProcessingServiceException =
                    new SystemProcessingServiceException(
                        message: "System processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedSystemProcessingServiceException);
            }
        }

        private async ValueTask<SystemProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var systemProcessingValidationException = new SystemProcessingValidationException(
                message: "System processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(systemProcessingValidationException);

            return systemProcessingValidationException;
        }

        private async ValueTask<SystemProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var systemProcessingDependencyValidationException =
                new SystemProcessingDependencyValidationException(
                    message: "System processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(systemProcessingDependencyValidationException);

            return systemProcessingDependencyValidationException;
        }

        private async ValueTask<SystemProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var systemProcessingDependencyException = new SystemProcessingDependencyException(
                message: "System processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(systemProcessingDependencyException);

            return systemProcessingDependencyException;
        }

        private async ValueTask<SystemProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var systemProcessingDependencyException = new SystemProcessingDependencyException(
                message: "System processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(systemProcessingDependencyException);

            return systemProcessingDependencyException;
        }

        private async ValueTask<SystemProcessingServiceException>
            CreateAndLogServiceExceptionAsync(SystemProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
