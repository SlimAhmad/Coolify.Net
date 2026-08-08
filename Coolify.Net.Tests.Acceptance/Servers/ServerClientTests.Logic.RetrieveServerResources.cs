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
        public async Task ShouldRetrieveServerResourcesAsync()
        {
            // given
            string serverUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/servers/{serverUuid}/resources").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "app-one", type = "application" }
                    }));

            // when
            IEnumerable<object> actualResources =
                await this.clientBroker.Client.Servers.RetrieveServerResourcesAsync(serverUuid);

            // then
            actualResources.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/servers/{serverUuid}/resources").UsingGet())
                .Should().ContainSingle();
        }
    }
}
