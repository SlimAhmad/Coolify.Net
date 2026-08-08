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
        public async Task ShouldRetrieveAllTeamsAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/teams").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[] { new { id = GetRandomId(), name = "team-one" } }));

            // when
            IEnumerable<Team> actualTeams =
                await this.clientBroker.Client.Teams.RetrieveAllTeamsAsync();

            // then
            actualTeams.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/teams").UsingGet())
                .Should().ContainSingle();
        }
    }
}
