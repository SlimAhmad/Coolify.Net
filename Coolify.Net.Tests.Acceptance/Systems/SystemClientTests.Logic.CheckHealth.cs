// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Systems
{
    public partial class SystemClientTests
    {
        [Fact]
        public async Task ShouldCheckHealthAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/healthcheck").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            bool isHealthy =
                await this.clientBroker.Client.System.CheckHealthAsync();

            // then
            isHealthy.Should().BeTrue();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/healthcheck").UsingGet())
                .Should().ContainSingle();
        }
    }
}
