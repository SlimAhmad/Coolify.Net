// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Foundations.Servers
{
    public partial class ServerService
    {
        private delegate ValueTask<T> ReturningServerFunction<T>();
        private delegate ValueTask ReturningNothingServerFunction();

        private async ValueTask<T> TryCatch<T>(ReturningServerFunction<T> returningServerFunction)
        {
            try
            {
                return await returningServerFunction();
            }
            catch (NullServerException nullServerException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullServerException);
            }
            catch (InvalidServerException invalidServerException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidServerException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidServerException =
                    new InvalidServerException(
                        message: "Invalid server.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidServerException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsServerException =
                    new AlreadyExistsServerException(
                        message: "Server already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsServerException);
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
                var failedServerDependencyException =
                    new FailedServerDependencyException(
                        message: "Failed server dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedServerDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedServerDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedServerDependencyException =
                    new FailedServerDependencyException(
                        message: "Failed server dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedServerDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutServerException =
                    new TimeoutServerException(
                        message: "Server dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutServerException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedServerServiceException =
                    new FailedServerServiceException(
                        message: "Failed server service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServerServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingServerFunction returningNothingServerFunction)
        {
            try
            {
                await returningNothingServerFunction();
            }
            catch (NullServerException nullServerException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullServerException);
            }
            catch (InvalidServerException invalidServerException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidServerException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedServerDependencyException =
                    new FailedServerDependencyException(
                        message: "Failed server dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyExceptionAsync(failedServerDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutServerException =
                    new TimeoutServerException(
                        message: "Server dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutServerException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedServerServiceException =
                    new FailedServerServiceException(
                        message: "Failed server service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedServerServiceException);
            }
        }

        private async ValueTask<ServerValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var serverValidationException = new ServerValidationException(
                message: "Server validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(serverValidationException);

            return serverValidationException;
        }

        private async ValueTask<ServerDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var serverDependencyValidationException = new ServerDependencyValidationException(
                message: "Server dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(serverDependencyValidationException);

            return serverDependencyValidationException;
        }

        private async ValueTask<ServerDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var serverDependencyException = new ServerDependencyException(
                message: "Server dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(serverDependencyException);

            return serverDependencyException;
        }

        private async ValueTask<ServerDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var serverDependencyException = new ServerDependencyException(
                message: "Server dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(serverDependencyException);

            return serverDependencyException;
        }

        private async ValueTask<ServerServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var serverServiceException = new ServerServiceException(
                message: "Server service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(serverServiceException);

            return serverServiceException;
        }
    }
}
