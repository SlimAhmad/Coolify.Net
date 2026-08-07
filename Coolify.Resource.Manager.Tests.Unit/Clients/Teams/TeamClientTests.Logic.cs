// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Teams
{
    public partial class TeamClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllTeamsAsync()
        {
            IEnumerable<Team> randomTeams = Enumerable.Range(0, 3).Select(_ => CreateRandomTeam());

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomTeams);

            IEnumerable<Team> actualTeams = await this.teamClient.RetrieveAllTeamsAsync();

            actualTeams.Should().BeEquivalentTo(randomTeams);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveCurrentTeamAsync()
        {
            Team randomTeam = CreateRandomTeam();

            this.teamServiceMock
                .Setup(service => service.RetrieveCurrentTeamAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomTeam);

            Team actualTeam = await this.teamClient.RetrieveCurrentTeamAsync();

            actualTeam.Should().BeEquivalentTo(randomTeam);

            this.teamServiceMock.Verify(
                service => service.RetrieveCurrentTeamAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveCurrentTeamMembersAsync()
        {
            IEnumerable<TeamMember> randomTeamMembers = Enumerable.Range(0, 3).Select(_ => CreateRandomTeamMember());

            this.teamServiceMock
                .Setup(service => service.RetrieveCurrentTeamMembersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomTeamMembers);

            IEnumerable<TeamMember> actualTeamMembers = await this.teamClient.RetrieveCurrentTeamMembersAsync();

            actualTeamMembers.Should().BeEquivalentTo(randomTeamMembers);

            this.teamServiceMock.Verify(
                service => service.RetrieveCurrentTeamMembersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveTeamByIdAsync()
        {
            Team randomTeam = CreateRandomTeam();
            int inputId = randomTeam.Id;

            this.teamServiceMock
                .Setup(service => service.RetrieveTeamByIdAsync(inputId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomTeam);

            Team actualTeam = await this.teamClient.RetrieveTeamByIdAsync(inputId);

            actualTeam.Should().BeEquivalentTo(randomTeam);

            this.teamServiceMock.Verify(service =>
                service.RetrieveTeamByIdAsync(inputId, It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveTeamMembersAsync()
        {
            int inputId = GetRandomId();
            IEnumerable<TeamMember> randomTeamMembers = Enumerable.Range(0, 3).Select(_ => CreateRandomTeamMember());

            this.teamServiceMock
                .Setup(service => service.RetrieveTeamMembersAsync(inputId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomTeamMembers);

            IEnumerable<TeamMember> actualTeamMembers = await this.teamClient.RetrieveTeamMembersAsync(inputId);

            actualTeamMembers.Should().BeEquivalentTo(randomTeamMembers);

            this.teamServiceMock.Verify(service =>
                service.RetrieveTeamMembersAsync(inputId, It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }
    }
}
