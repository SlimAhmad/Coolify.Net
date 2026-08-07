// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Teams;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalTeam>> GetAllTeamsAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalTeam> GetCurrentTeamAsync(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalTeamMember>> GetCurrentTeamMembersAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalTeam> GetTeamByIdAsync(int id, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalTeamMember>> GetTeamMembersAsync(int id, CancellationToken cancellationToken = default);
    }
}
