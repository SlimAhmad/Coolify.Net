// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Teams;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Teams
{
    public partial class TeamClientTests
    {
        [Fact]
        public async Task ShouldRetrieveCurrentTeamMembersAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/teams/current/members").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "Jane Doe", role = "owner" }
                    }));

            // when
            IEnumerable<TeamMember> actualMembers =
                await this.clientBroker.Client.Teams.RetrieveCurrentTeamMembersAsync();

            // then
            actualMembers.Should().ContainSingle(member => member.Role == "owner");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/teams/current/members").UsingGet())
                .Should().ContainSingle();
        }
    }
}
