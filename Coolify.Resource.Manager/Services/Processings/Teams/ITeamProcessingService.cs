// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams;

namespace Coolify.Resource.Manager.Services.Processings.Teams
{
    public interface ITeamProcessingService
    {
        ValueTask<IEnumerable<Team>> RetrieveAllTeamsAsync(CancellationToken cancellationToken = default);
        ValueTask<Team> RetrieveCurrentTeamAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<TeamMember>> RetrieveCurrentTeamMembersAsync(CancellationToken cancellationToken = default);
        ValueTask<Team> RetrieveTeamByIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<TeamMember>> RetrieveTeamMembersAsync(int id, CancellationToken cancellationToken = default);
    }
}
