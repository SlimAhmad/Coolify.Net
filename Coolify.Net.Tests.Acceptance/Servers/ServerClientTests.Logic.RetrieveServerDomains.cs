// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldRetrieveServerDomainsAsync()
        {
            // given
            string serverUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/servers/{serverUuid}/domains").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[] { "example.com", "www.example.com" }));

            // when
            IEnumerable<string> actualDomains =
                await this.clientBroker.Client.Servers.RetrieveServerDomainsAsync(serverUuid);

            // then
            actualDomains.Should().Contain("example.com");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/servers/{serverUuid}/domains").UsingGet())
                .Should().ContainSingle();
        }
    }
}
