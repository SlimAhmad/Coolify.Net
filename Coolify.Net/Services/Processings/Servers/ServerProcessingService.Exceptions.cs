// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Coolify.Net.Models.Processings.Servers.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Processings.Servers
{
    public partial class ServerProcessingService
    {
        private delegate ValueTask<T> ReturningServerProcessingFunction<T>();
        private delegate ValueTask ReturningNothingServerProcessingFunction();

        private async ValueTask<T> TryCatch<T>(ReturningServerProcessingFunction<T> returningServerProcessingFunction)
        {
            try
            {
                return await returningServerProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutServerProcessingException =
                    new TimeoutServerProcessingException(
                        message: "Server processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutServerProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullServerProcessingException nullServerProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullServerProcessingException);
            }
            catch (InvalidServerProcessingException invalidServerProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidServerProcessingException);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(serverValidationException);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(serverDependencyValidationException);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(serverDependencyException);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(serverServiceException);
            }
            catch (Exception exception)
            {
                var failedServerProcessingServiceException =
                    new ServerProcessingServiceException(
                        message: "Server processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServerProcessingServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingServerProcessingFunction returningNothingServerProcessingFunction)
        {
            try
            {
                await returningNothingServerProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutServerProcessingException =
                    new TimeoutServerProcessingException(
                        message: "Server processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutServerProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullServerProcessingException nullServerProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullServerProcessingException);
            }
            catch (InvalidServerProcessingException invalidServerProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidServerProcessingException);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(serverValidationException);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(serverDependencyValidationException);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(serverDependencyException);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(serverServiceException);
            }
            catch (Exception exception)
            {
                var failedServerProcessingServiceException =
                    new ServerProcessingServiceException(
                        message: "Server processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServerProcessingServiceException);
            }
        }

        private async ValueTask<ServerProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var serverProcessingValidationException = new ServerProcessingValidationException(
                message: "Server processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(serverProcessingValidationException);

            return serverProcessingValidationException;
        }

        private async ValueTask<ServerProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var serverProcessingDependencyValidationException = new ServerProcessingDependencyValidationException(
                message: "Server processing dependency validation error occurred, fix the errors and try again.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(serverProcessingDependencyValidationException);

            return serverProcessingDependencyValidationException;
        }

        private async ValueTask<ServerProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var serverProcessingDependencyException = new ServerProcessingDependencyException(
                message: "Server processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(serverProcessingDependencyException);

            return serverProcessingDependencyException;
        }

        private async ValueTask<ServerProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var serverProcessingDependencyException = new ServerProcessingDependencyException(
                message: "Server processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(serverProcessingDependencyException);

            return serverProcessingDependencyException;
        }

        private async ValueTask<ServerProcessingServiceException>
            CreateAndLogServiceExceptionAsync(ServerProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
