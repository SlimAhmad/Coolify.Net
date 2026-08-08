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
        public async Task ShouldRetrieveTeamByIdAsync()
        {
            // given
            int someId = GetRandomId();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/teams/{someId}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { id = someId, name = "acceptance-team" }));

            // when
            Team actualTeam =
                await this.clientBroker.Client.Teams.RetrieveTeamByIdAsync(someId);

            // then
            actualTeam.Id.Should().Be(someId);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/teams/{someId}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
