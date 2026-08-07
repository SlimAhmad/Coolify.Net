// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Teams;
using Coolify.Resource.Manager.Services.Foundations.Teams;

namespace Coolify.Resource.Manager.Services.Processings.Teams
{
    public partial class TeamProcessingService : ITeamProcessingService
    {
        private readonly ITeamService teamService;
        private readonly ILoggingBroker loggingBroker;

        public TeamProcessingService(
            ITeamService teamService,
            ILoggingBroker loggingBroker)
        {
            this.teamService = teamService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Team>> RetrieveAllTeamsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.teamService.RetrieveAllTeamsAsync(cancellationToken);
                });

        public ValueTask<Team> RetrieveCurrentTeamAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.teamService.RetrieveCurrentTeamAsync(cancellationToken);
                });

        public ValueTask<IEnumerable<TeamMember>> RetrieveCurrentTeamMembersAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.teamService.RetrieveCurrentTeamMembersAsync(cancellationToken);
                });

        public ValueTask<Team> RetrieveTeamByIdAsync(
            int id, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateTeamId(id);

                    return await this.teamService.RetrieveTeamByIdAsync(id, cancellationToken);
                });

        public ValueTask<IEnumerable<TeamMember>> RetrieveTeamMembersAsync(
            int id, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateTeamId(id);

                    return await this.teamService.RetrieveTeamMembersAsync(id, cancellationToken);
                });
    }
}
