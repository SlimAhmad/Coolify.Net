// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams;

namespace Coolify.Resource.Manager.Clients.Teams
{
    /// <summary>Defines the contract for reading Coolify teams and their members.</summary>
    public interface ITeamClient
    {
        /// <summary>Retrieves all teams accessible by the configured account.</summary>
        /// <exception cref="Exceptions.TeamClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.TeamClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.TeamClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<Team>> RetrieveAllTeamsAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves the currently authenticated team.</summary>
        ValueTask<Team> RetrieveCurrentTeamAsync(CancellationToken cancellationToken = default);

        /// <summary>Lists members of the currently authenticated team.</summary>
        ValueTask<IEnumerable<TeamMember>> RetrieveCurrentTeamMembersAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a team by its id.</summary>
        ValueTask<Team> RetrieveTeamByIdAsync(int id, CancellationToken cancellationToken = default);

        /// <summary>Lists members of a team by its id.</summary>
        ValueTask<IEnumerable<TeamMember>> RetrieveTeamMembersAsync(int id, CancellationToken cancellationToken = default);
    }
}
