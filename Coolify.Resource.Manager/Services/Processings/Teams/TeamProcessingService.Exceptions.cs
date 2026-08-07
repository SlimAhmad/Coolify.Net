// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams.Exceptions;
using Coolify.Resource.Manager.Models.Processings.Teams.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Processings.Teams
{
    public partial class TeamProcessingService
    {
        private delegate ValueTask<T> ReturningTeamProcessingFunction<T>();

        private async ValueTask<T> TryCatch<T>(
            ReturningTeamProcessingFunction<T> returningTeamProcessingFunction)
        {
            try
            {
                return await returningTeamProcessingFunction();
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutTeamProcessingException =
                    new TimeoutTeamProcessingException(
                        message: "Team processing timeout error occurred, contact support.",
                        innerException: operationCanceledException);

                throw await CreateAndLogTimeoutDependencyExceptionAsync(timeoutTeamProcessingException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (NullTeamProcessingException nullTeamProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullTeamProcessingException);
            }
            catch (InvalidTeamProcessingException invalidTeamProcessingException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidTeamProcessingException);
            }
            catch (TeamValidationException teamValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(teamValidationException);
            }
            catch (TeamDependencyValidationException teamDependencyValidationException)
            {
                throw await CreateAndLogDependencyValidationExceptionAsync(teamDependencyValidationException);
            }
            catch (TeamDependencyException teamDependencyException)
            {
                throw await CreateAndLogDependencyExceptionAsync(teamDependencyException);
            }
            catch (TeamServiceException teamServiceException)
            {
                throw await CreateAndLogDependencyExceptionAsync(teamServiceException);
            }
            catch (Exception exception)
            {
                var failedTeamProcessingServiceException =
                    new TeamProcessingServiceException(
                        message: "Team processing service error occurred, contact support.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedTeamProcessingServiceException);
            }
        }

        private async ValueTask<TeamProcessingValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var teamProcessingValidationException = new TeamProcessingValidationException(
                message: "Team processing validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(teamProcessingValidationException);

            return teamProcessingValidationException;
        }

        private async ValueTask<TeamProcessingDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var teamProcessingDependencyValidationException =
                new TeamProcessingDependencyValidationException(
                    message: "Team processing dependency validation error occurred, fix the errors and try again.",
                    innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(teamProcessingDependencyValidationException);

            return teamProcessingDependencyValidationException;
        }

        private async ValueTask<TeamProcessingDependencyException>
            CreateAndLogTimeoutDependencyExceptionAsync(Xeption exception)
        {
            var teamProcessingDependencyException = new TeamProcessingDependencyException(
                message: "Team processing dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(teamProcessingDependencyException);

            return teamProcessingDependencyException;
        }

        private async ValueTask<TeamProcessingDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var teamProcessingDependencyException = new TeamProcessingDependencyException(
                message: "Team processing dependency error occurred, contact support.",
                innerException: exception.InnerException as Xeption);

            await this.loggingBroker.LogErrorAsync(teamProcessingDependencyException);

            return teamProcessingDependencyException;
        }

        private async ValueTask<TeamProcessingServiceException>
            CreateAndLogServiceExceptionAsync(TeamProcessingServiceException exception)
        {
            await this.loggingBroker.LogErrorAsync(exception);

            return exception;
        }
    }
}
