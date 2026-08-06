// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions;
using Coolify.Resource.Manager.Models.Processings.Applications.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Processings.Applications
{
    public partial class ApplicationProcessingService
    {
        private delegate ValueTask<T> ReturningApplicationProcessingFunction<T>();
        private delegate ValueTask ReturningNothingApplicationProcessingFunction();

        private async ValueTask<T> TryCatch<T>(
            ReturningApplicationProcessingFunction<T> returningApplicationProcessingFunction)
        {
            try
            {
                return await returningApplicationProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutApplicationProcessingException =
                    new TimeoutApplicationProcessingException(
                        message: "Application processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutApplicationProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullApplicationProcessingException nullApplicationProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullApplicationProcessingException);
            }
            catch (InvalidApplicationProcessingException invalidApplicationProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidApplicationProcessingException);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(applicationValidationException);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(applicationDependencyValidationException);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(applicationDependencyException);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(applicationServiceException);
            }
            catch (Exception exception)
            {
                var failedApplicationProcessingServiceException =
                    new ApplicationProcessingServiceException(
                        message: "Application processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedApplicationProcessingServiceException);
            }
        }

        private async ValueTask TryCatch(
            ReturningNothingApplicationProcessingFunction returningNothingApplicationProcessingFunction)
        {
            try
            {
                await returningNothingApplicationProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutApplicationProcessingException =
                    new TimeoutApplicationProcessingException(
                        message: "Application processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutApplicationProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullApplicationProcessingException nullApplicationProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullApplicationProcessingException);
            }
            catch (InvalidApplicationProcessingException invalidApplicationProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidApplicationProcessingException);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(applicationValidationException);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(applicationDependencyValidationException);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(applicationDependencyException);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(applicationServiceException);
            }
            catch (Exception exception)
            {
                var failedApplicationProcessingServiceException =
                    new ApplicationProcessingServiceException(
                        message: "Application processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedApplicationProcessingServiceException);
            }
        }

        private async ValueTask<ApplicationProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var applicationProcessingValidationException = new ApplicationProcessingValidationException(
                message: "Application processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(applicationProcessingValidationException);

            return applicationProcessingValidationException;
        }

        private async ValueTask<ApplicationProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var applicationProcessingDependencyValidationException =
                new ApplicationProcessingDependencyValidationException(
                    message: "Application processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(applicationProcessingDependencyValidationException);

            return applicationProcessingDependencyValidationException;
        }

        private async ValueTask<ApplicationProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var applicationProcessingDependencyException = new ApplicationProcessingDependencyException(
                message: "Application processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(applicationProcessingDependencyException);

            return applicationProcessingDependencyException;
        }

        private async ValueTask<ApplicationProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var applicationProcessingDependencyException = new ApplicationProcessingDependencyException(
                message: "Application processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(applicationProcessingDependencyException);

            return applicationProcessingDependencyException;
        }

        private async ValueTask<ApplicationProcessingServiceException>
            CreateAndLogServiceExceptionAsync(ApplicationProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
