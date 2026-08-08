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
        public async Task ShouldRetrieveCurrentTeamAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/teams/current").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { id = GetRandomId(), name = "Acceptance Team" }));

            // when
            Team actualTeam =
                await this.clientBroker.Client.Teams.RetrieveCurrentTeamAsync();

            // then
            actualTeam.Name.Should().Be("Acceptance Team");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/teams/current").UsingGet())
                .Should().ContainSingle();
        }
    }
}
