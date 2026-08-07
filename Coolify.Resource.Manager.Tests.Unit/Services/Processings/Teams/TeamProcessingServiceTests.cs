// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Teams;
using Coolify.Resource.Manager.Services.Foundations.Teams;
using Coolify.Resource.Manager.Services.Processings.Teams;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Teams
{
    public partial class TeamProcessingServiceTests
    {
        private readonly Mock<ITeamService> teamServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITeamProcessingService teamProcessingService;

        public TeamProcessingServiceTests()
        {
            this.teamServiceMock = new Mock<ITeamService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.teamProcessingService = new TeamProcessingService(
                teamService: this.teamServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static int GetRandomId() => new Random().Next(1, 1000);

        private static Team CreateRandomTeam() =>
            new Team { Id = GetRandomId(), Name = GetRandomString() };

        private static TeamMember CreateRandomTeamMember() =>
            new TeamMember { Uuid = GetRandomString(), Name = GetRandomString() };
    }
}
