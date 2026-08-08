// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Teams;

namespace Coolify.Net.Services.Processings.Teams
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
