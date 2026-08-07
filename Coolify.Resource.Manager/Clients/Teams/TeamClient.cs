// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Teams.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Teams;
using Coolify.Resource.Manager.Models.Processings.Teams.Exceptions;
using Coolify.Resource.Manager.Services.Processings.Teams;
using Xeptions;

namespace Coolify.Resource.Manager.Clients.Teams
{
    /// <summary>Provides read access to Coolify teams and their members.</summary>
    internal class TeamClient : ITeamClient
    {
        private readonly ITeamProcessingService teamProcessingService;

        public TeamClient(ITeamProcessingService teamProcessingService) =>
            this.teamProcessingService = teamProcessingService;

        public async ValueTask<IEnumerable<Team>> RetrieveAllTeamsAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.teamProcessingService.RetrieveAllTeamsAsync(cancellationToken);
            }
            catch (TeamProcessingValidationException teamValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyValidationException teamDependencyValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamDependencyValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyException teamDependencyException)
            {
                throw CreateTeamClientDependencyException(
                    teamDependencyException.InnerException as Xeption);
            }
            catch (TeamProcessingServiceException teamServiceException)
            {
                throw CreateTeamClientDependencyException(
                    teamServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateTeamClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Team> RetrieveCurrentTeamAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.teamProcessingService.RetrieveCurrentTeamAsync(cancellationToken);
            }
            catch (TeamProcessingValidationException teamValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyValidationException teamDependencyValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamDependencyValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyException teamDependencyException)
            {
                throw CreateTeamClientDependencyException(
                    teamDependencyException.InnerException as Xeption);
            }
            catch (TeamProcessingServiceException teamServiceException)
            {
                throw CreateTeamClientDependencyException(
                    teamServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateTeamClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<TeamMember>> RetrieveCurrentTeamMembersAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.teamProcessingService.RetrieveCurrentTeamMembersAsync(cancellationToken);
            }
            catch (TeamProcessingValidationException teamValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyValidationException teamDependencyValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamDependencyValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyException teamDependencyException)
            {
                throw CreateTeamClientDependencyException(
                    teamDependencyException.InnerException as Xeption);
            }
            catch (TeamProcessingServiceException teamServiceException)
            {
                throw CreateTeamClientDependencyException(
                    teamServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateTeamClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Team> RetrieveTeamByIdAsync(
            int id, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.teamProcessingService.RetrieveTeamByIdAsync(id, cancellationToken);
            }
            catch (TeamProcessingValidationException teamValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyValidationException teamDependencyValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamDependencyValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyException teamDependencyException)
            {
                throw CreateTeamClientDependencyException(
                    teamDependencyException.InnerException as Xeption);
            }
            catch (TeamProcessingServiceException teamServiceException)
            {
                throw CreateTeamClientDependencyException(
                    teamServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateTeamClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<TeamMember>> RetrieveTeamMembersAsync(
            int id, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.teamProcessingService.RetrieveTeamMembersAsync(id, cancellationToken);
            }
            catch (TeamProcessingValidationException teamValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyValidationException teamDependencyValidationException)
            {
                throw CreateTeamClientValidationException(
                    teamDependencyValidationException.InnerException as Xeption);
            }
            catch (TeamProcessingDependencyException teamDependencyException)
            {
                throw CreateTeamClientDependencyException(
                    teamDependencyException.InnerException as Xeption);
            }
            catch (TeamProcessingServiceException teamServiceException)
            {
                throw CreateTeamClientDependencyException(
                    teamServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateTeamClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static TeamClientValidationException
            CreateTeamClientValidationException(Xeption innerException)
        {
            return new TeamClientValidationException(
                message: "Team client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static TeamClientDependencyException
            CreateTeamClientDependencyException(Xeption innerException)
        {
            return new TeamClientDependencyException(
                message: "Team client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static TeamClientServiceException
            CreateTeamClientServiceException(Xeption innerException)
        {
            return new TeamClientServiceException(
                message: "Team client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
