// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Teams;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string TeamsRelativeUrl = "teams";

        public async ValueTask<IEnumerable<ExternalTeam>> GetAllTeamsAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalTeam>>(TeamsRelativeUrl, cancellationToken);

        public async ValueTask<ExternalTeam> GetCurrentTeamAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalTeam>($"{TeamsRelativeUrl}/current", cancellationToken);

        public async ValueTask<IEnumerable<ExternalTeamMember>> GetCurrentTeamMembersAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalTeamMember>>(
                    $"{TeamsRelativeUrl}/current/members", cancellationToken);

        public async ValueTask<ExternalTeam> GetTeamByIdAsync(
            int id, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalTeam>($"{TeamsRelativeUrl}/{id}", cancellationToken);

        public async ValueTask<IEnumerable<ExternalTeamMember>> GetTeamMembersAsync(
            int id, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalTeamMember>>($"{TeamsRelativeUrl}/{id}/members", cancellationToken);
    }
}
