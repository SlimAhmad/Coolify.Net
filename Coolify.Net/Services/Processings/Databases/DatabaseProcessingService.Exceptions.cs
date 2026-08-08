// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Databases.Exceptions;
using Coolify.Net.Models.Processings.Databases.Exceptions;
using Xeptions;

namespace Coolify.Net.Services.Processings.Databases
{
    public partial class DatabaseProcessingService
    {
        private delegate ValueTask<T> ReturningDatabaseProcessingFunction<T>();
        private delegate ValueTask ReturningNothingDatabaseProcessingFunction();

        private async ValueTask<T> TryCatch<T>(ReturningDatabaseProcessingFunction<T> returningDatabaseProcessingFunction)
        {
            try
            {
                return await returningDatabaseProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutDatabaseProcessingException =
                    new TimeoutDatabaseProcessingException(
                        message: "Database processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutDatabaseProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullDatabaseProcessingException nullDatabaseProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullDatabaseProcessingException);
            }
            catch (InvalidDatabaseProcessingException invalidDatabaseProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidDatabaseProcessingException);
            }
            catch (DatabaseValidationException databaseValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(databaseValidationException);
            }
            catch (DatabaseDependencyValidationException databaseDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(databaseDependencyValidationException);
            }
            catch (DatabaseDependencyException databaseDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(databaseDependencyException);
            }
            catch (DatabaseServiceException databaseServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(databaseServiceException);
            }
            catch (Exception exception)
            {
                var failedDatabaseProcessingServiceException =
                    new DatabaseProcessingServiceException(
                        message: "Database processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedDatabaseProcessingServiceException);
            }
        }

        private async ValueTask TryCatch(ReturningNothingDatabaseProcessingFunction returningNothingDatabaseProcessingFunction)
        {
            try
            {
                await returningNothingDatabaseProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutDatabaseProcessingException =
                    new TimeoutDatabaseProcessingException(
                        message: "Database processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutDatabaseProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullDatabaseProcessingException nullDatabaseProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullDatabaseProcessingException);
            }
            catch (InvalidDatabaseProcessingException invalidDatabaseProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidDatabaseProcessingException);
            }
            catch (DatabaseValidationException databaseValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(databaseValidationException);
            }
            catch (DatabaseDependencyValidationException databaseDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(databaseDependencyValidationException);
            }
            catch (DatabaseDependencyException databaseDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(databaseDependencyException);
            }
            catch (DatabaseServiceException databaseServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(databaseServiceException);
            }
            catch (Exception exception)
            {
                var failedDatabaseProcessingServiceException =
                    new DatabaseProcessingServiceException(
                        message: "Database processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedDatabaseProcessingServiceException);
            }
        }

        private async ValueTask<DatabaseProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var databaseProcessingValidationException = new DatabaseProcessingValidationException(
                message: "Database processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(databaseProcessingValidationException);

            return databaseProcessingValidationException;
        }

        private async ValueTask<DatabaseProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var databaseProcessingDependencyValidationException =
                new DatabaseProcessingDependencyValidationException(
                    message: "Database processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(databaseProcessingDependencyValidationException);

            return databaseProcessingDependencyValidationException;
        }

        private async ValueTask<DatabaseProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var databaseProcessingDependencyException = new DatabaseProcessingDependencyException(
                message: "Database processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(databaseProcessingDependencyException);

            return databaseProcessingDependencyException;
        }

        private async ValueTask<DatabaseProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var databaseProcessingDependencyException = new DatabaseProcessingDependencyException(
                message: "Database processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(databaseProcessingDependencyException);

            return databaseProcessingDependencyException;
        }

        private async ValueTask<DatabaseProcessingServiceException>
            CreateAndLogServiceExceptionAsync(DatabaseProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
