// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Teams;
using Coolify.Net.Models.Foundations.Teams;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllTeamsAsync()
        {
            List<ExternalTeam> randomExternalTeams =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalTeam()).ToList();

            IEnumerable<Team> expectedTeams = randomExternalTeams.Select(ConvertToTeam);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalTeams);

            IEnumerable<Team> actualTeams = await this.teamService.RetrieveAllTeamsAsync();

            actualTeams.Should().BeEquivalentTo(expectedTeams);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveCurrentTeamAsync()
        {
            ExternalTeam randomExternalTeam = CreateRandomExternalTeam();
            Team expectedTeam = ConvertToTeam(randomExternalTeam);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalTeam);

            Team actualTeam = await this.teamService.RetrieveCurrentTeamAsync();

            actualTeam.Should().BeEquivalentTo(expectedTeam);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetCurrentTeamAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveCurrentTeamMembersAsync()
        {
            List<ExternalTeamMember> randomExternalTeamMembers =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalTeamMember()).ToList();

            IEnumerable<TeamMember> expectedTeamMembers = randomExternalTeamMembers.Select(ConvertToTeamMember);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalTeamMembers);

            IEnumerable<TeamMember> actualTeamMembers = await this.teamService.RetrieveCurrentTeamMembersAsync();

            actualTeamMembers.Should().BeEquivalentTo(expectedTeamMembers);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetCurrentTeamMembersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveTeamByIdAsync()
        {
            ExternalTeam randomExternalTeam = CreateRandomExternalTeam();
            int inputId = randomExternalTeam.Id;
            Team expectedTeam = ConvertToTeam(randomExternalTeam);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(inputId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalTeam);

            Team actualTeam = await this.teamService.RetrieveTeamByIdAsync(inputId);

            actualTeam.Should().BeEquivalentTo(expectedTeam);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(inputId, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveTeamMembersAsync()
        {
            int inputId = GetRandomId();

            List<ExternalTeamMember> randomExternalTeamMembers =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalTeamMember()).ToList();

            IEnumerable<TeamMember> expectedTeamMembers = randomExternalTeamMembers.Select(ConvertToTeamMember);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(inputId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalTeamMembers);

            IEnumerable<TeamMember> actualTeamMembers = await this.teamService.RetrieveTeamMembersAsync(inputId);

            actualTeamMembers.Should().BeEquivalentTo(expectedTeamMembers);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(inputId, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
