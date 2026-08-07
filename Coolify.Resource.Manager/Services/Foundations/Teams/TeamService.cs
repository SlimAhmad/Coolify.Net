// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Teams;
using Coolify.Resource.Manager.Models.Foundations.Teams;

namespace Coolify.Resource.Manager.Services.Foundations.Teams
{
    public partial class TeamService : ITeamService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public TeamService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Team>> RetrieveAllTeamsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalTeam> externalTeams =
                        await this.coolifyApiBroker.GetAllTeamsAsync(cancellationToken);

                    return externalTeams.Select(ConvertToTeam);
                });

        public ValueTask<Team> RetrieveCurrentTeamAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    ExternalTeam externalTeam =
                        await this.coolifyApiBroker.GetCurrentTeamAsync(cancellationToken);

                    return ConvertToTeam(externalTeam);
                });

        public ValueTask<IEnumerable<TeamMember>> RetrieveCurrentTeamMembersAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalTeamMember> externalTeamMembers =
                        await this.coolifyApiBroker.GetCurrentTeamMembersAsync(cancellationToken);

                    return externalTeamMembers.Select(ConvertToTeamMember);
                });

        public ValueTask<Team> RetrieveTeamByIdAsync(
            int id, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateTeamId(id);

                    ExternalTeam externalTeam =
                        await this.coolifyApiBroker.GetTeamByIdAsync(id, cancellationToken);

                    return ConvertToTeam(externalTeam);
                });

        public ValueTask<IEnumerable<TeamMember>> RetrieveTeamMembersAsync(
            int id, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateTeamId(id);

                    IEnumerable<ExternalTeamMember> externalTeamMembers =
                        await this.coolifyApiBroker.GetTeamMembersAsync(id, cancellationToken);

                    return externalTeamMembers.Select(ConvertToTeamMember);
                });

        // ---- Conversion helpers ----

        private static Team ConvertToTeam(ExternalTeam externalTeam) =>
            new Team
            {
                Id = externalTeam.Id,
                Name = externalTeam.Name,
                Description = externalTeam.Description,
                CreatedAt = externalTeam.CreatedAt,
                UpdatedAt = externalTeam.UpdatedAt
            };

        private static TeamMember ConvertToTeamMember(ExternalTeamMember externalTeamMember) =>
            new TeamMember
            {
                Uuid = externalTeamMember.Uuid,
                Name = externalTeamMember.Name,
                Email = externalTeamMember.Email,
                Role = externalTeamMember.Role,
                CreatedAt = externalTeamMember.CreatedAt,
                UpdatedAt = externalTeamMember.UpdatedAt
            };
    }
}
