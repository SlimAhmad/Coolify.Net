// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Foundations.Databases
{
    public partial class DatabaseService
    {
        private delegate ValueTask<T> ReturningDatabaseFunction<T>();
        private delegate ValueTask ReturningNothingDatabaseFunction();

        private async ValueTask<T> TryCatch<T>(ReturningDatabaseFunction<T> returningDatabaseFunction)
        {
            try
            {
                return await returningDatabaseFunction();
            }
            catch (NullDatabaseException nullDatabaseException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullDatabaseException);
            }
            catch (InvalidDatabaseException invalidDatabaseException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidDatabaseException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidDatabaseException =
                    new InvalidDatabaseException(
                        message: "Invalid database.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidDatabaseException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsDatabaseException =
                    new AlreadyExistsDatabaseException(
                        message: "Database already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsDatabaseException);
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
                var failedDatabaseDependencyException =
                    new FailedDatabaseDependencyException(
                        message: "Failed database dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedDatabaseDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedDatabaseDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedDatabaseDependencyException =
                    new FailedDatabaseDependencyException(
                        message: "Failed database dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedDatabaseDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutDatabaseException =
                    new TimeoutDatabaseException(
                        message: "Database dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutDatabaseException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedDatabaseServiceException =
                    new FailedDatabaseServiceException(
                        message: "Failed database service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedDatabaseServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingDatabaseFunction returningNothingDatabaseFunction)
        {
            try
            {
                await returningNothingDatabaseFunction();
            }
            catch (NullDatabaseException nullDatabaseException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullDatabaseException);
            }
            catch (InvalidDatabaseException invalidDatabaseException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidDatabaseException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedDatabaseDependencyException =
                    new FailedDatabaseDependencyException(
                        message: "Failed database dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyExceptionAsync(failedDatabaseDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutDatabaseException =
                    new TimeoutDatabaseException(
                        message: "Database dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutDatabaseException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedDatabaseServiceException =
                    new FailedDatabaseServiceException(
                        message: "Failed database service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedDatabaseServiceException);
            }
        }

        private async ValueTask<DatabaseValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var databaseValidationException = new DatabaseValidationException(
                message: "Database validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(databaseValidationException);

            return databaseValidationException;
        }

        private async ValueTask<DatabaseDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var databaseDependencyValidationException = new DatabaseDependencyValidationException(
                message: "Database dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(databaseDependencyValidationException);

            return databaseDependencyValidationException;
        }

        private async ValueTask<DatabaseDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var databaseDependencyException = new DatabaseDependencyException(
                message: "Database dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(databaseDependencyException);

            return databaseDependencyException;
        }

        private async ValueTask<DatabaseDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var databaseDependencyException = new DatabaseDependencyException(
                message: "Database dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(databaseDependencyException);

            return databaseDependencyException;
        }

        private async ValueTask<DatabaseServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var databaseServiceException = new DatabaseServiceException(
                message: "Database service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(databaseServiceException);

            return databaseServiceException;
        }
    }
}
