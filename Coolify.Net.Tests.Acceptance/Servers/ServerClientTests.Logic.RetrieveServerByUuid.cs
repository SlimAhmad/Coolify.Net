// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldRetrieveServerByUuidAsync()
        {
            // given
            string serverUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = serverUuid,
                        name = "acceptance-server",
                        ip = "10.0.0.1",
                        user = "root",
                        port = 22,
                        is_reachable = true,
                        is_usable = true
                    }));

            // when
            Server actualServer =
                await this.clientBroker.Client.Servers.RetrieveServerByUuidAsync(serverUuid);

            // then
            actualServer.Uuid.Should().Be(serverUuid);
            actualServer.IsReachable.Should().BeTrue();
            actualServer.IsUsable.Should().BeTrue();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
