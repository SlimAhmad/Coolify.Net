// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Teams
{
    public class TeamClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public TeamClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldRetrieveCurrentTeamAndItsMembersAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/teams/current").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { id = 1, name = "Acceptance Team" }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/teams/current/members").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "Jane Doe", role = "owner" }
                    }));

            // when
            Team currentTeam =
                await this.apiFixture.Client.Teams.RetrieveCurrentTeamAsync();

            IEnumerable<TeamMember> members =
                await this.apiFixture.Client.Teams.RetrieveCurrentTeamMembersAsync();

            // then
            currentTeam.Name.Should().Be("Acceptance Team");
            members.Should().ContainSingle(member => member.Role == "owner");
        }

        [Fact]
        public async Task ShouldRetrieveAllTeamsAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/teams").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[] { new { id = 1, name = "team-one" } }));

            // when
            IEnumerable<Team> actualTeams =
                await this.apiFixture.Client.Teams.RetrieveAllTeamsAsync();

            // then
            actualTeams.Should().ContainSingle();
        }
    }
}
