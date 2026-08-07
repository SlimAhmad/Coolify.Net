// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Teams.Exceptions;
using Xeptions;

namespace Coolify.Resource.Manager.Services.Foundations.Teams
{
    public partial class TeamService
    {
        private delegate ValueTask<T> ReturningTeamFunction<T>();

        private async ValueTask<T> TryCatch<T>(ReturningTeamFunction<T> returningTeamFunction)
        {
            try
            {
                return await returningTeamFunction();
            }
            catch (NullTeamException nullTeamException)
            {
                throw await CreateAndLogValidationExceptionAsync(nullTeamException);
            }
            catch (InvalidTeamException invalidTeamException)
            {
                throw await CreateAndLogValidationExceptionAsync(invalidTeamException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.BadRequest)
            {
                var invalidTeamException =
                    new InvalidTeamException(
                        message: "Invalid team.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(invalidTeamException);
            }
            catch (HttpRequestException httpRequestException)
                when (httpRequestException.StatusCode == HttpStatusCode.Conflict)
            {
                var alreadyExistsTeamException =
                    new AlreadyExistsTeamException(
                        message: "Team already exists.",
                        innerException: httpRequestException);

                throw await CreateAndLogDependencyValidationExceptionAsync(alreadyExistsTeamException);
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
                var failedTeamDependencyException =
                    new FailedTeamDependencyException(
                        message: "Failed team dependency error occurred.",
                        innerException: httpRequestException);

                bool isCritical =
                    httpRequestException.StatusCode is
                        HttpStatusCode.Unauthorized or
                        HttpStatusCode.Forbidden or
                        HttpStatusCode.NotFound;

                throw isCritical
                    ? await CreateAndLogCriticalDependencyExceptionAsync(failedTeamDependencyException)
                    : await CreateAndLogDependencyExceptionAsync(failedTeamDependencyException);
            }
            catch (HttpRequestException httpRequestException)
            {
                var failedTeamDependencyException =
                    new FailedTeamDependencyException(
                        message: "Failed team dependency error occurred.",
                        innerException: httpRequestException);

                throw await CreateAndLogCriticalDependencyExceptionAsync(failedTeamDependencyException);
            }
            catch (OperationCanceledException operationCanceledException)
                when (operationCanceledException.CancellationToken.IsCancellationRequested is false)
            {
                var timeoutTeamException =
                    new TimeoutTeamException(
                        message: "Team dependency timeout error occurred.",
                        innerException: operationCanceledException);

                throw await CreateAndLogDependencyExceptionAsync(timeoutTeamException);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                var failedTeamServiceException =
                    new FailedTeamServiceException(
                        message: "Failed team service error occurred.",
                        innerException: exception);

                throw await CreateAndLogServiceExceptionAsync(failedTeamServiceException);
            }
        }

        private async ValueTask<TeamValidationException>
            CreateAndLogValidationExceptionAsync(Xeption exception)
        {
            var teamValidationException = new TeamValidationException(
                message: "Team validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(teamValidationException);

            return teamValidationException;
        }

        private async ValueTask<TeamDependencyValidationException>
            CreateAndLogDependencyValidationExceptionAsync(Xeption exception)
        {
            var teamDependencyValidationException = new TeamDependencyValidationException(
                message: "Team dependency validation error occurred, fix the errors and try again.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(teamDependencyValidationException);

            return teamDependencyValidationException;
        }

        private async ValueTask<TeamDependencyException>
            CreateAndLogDependencyExceptionAsync(Xeption exception)
        {
            var teamDependencyException = new TeamDependencyException(
                message: "Team dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(teamDependencyException);

            return teamDependencyException;
        }

        private async ValueTask<TeamDependencyException>
            CreateAndLogCriticalDependencyExceptionAsync(Xeption exception)
        {
            var teamDependencyException = new TeamDependencyException(
                message: "Team dependency error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogCriticalAsync(teamDependencyException);

            return teamDependencyException;
        }

        private async ValueTask<TeamServiceException>
            CreateAndLogServiceExceptionAsync(Xeption exception)
        {
            var teamServiceException = new TeamServiceException(
                message: "Team service error occurred, contact support.",
                innerException: exception);

            await this.loggingBroker.LogErrorAsync(teamServiceException);

            return teamServiceException;
        }
    }
}
