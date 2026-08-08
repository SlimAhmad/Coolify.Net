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
        public async Task ShouldRetrieveAllServersAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/servers").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "server-one" },
                        new { uuid = GetRandomString(), name = "server-two" }
                    }));

            // when
            IEnumerable<Server> actualServers =
                await this.clientBroker.Client.Servers.RetrieveAllServersAsync();

            // then
            actualServers.Should().HaveCount(2);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/servers").UsingGet())
                .Should().ContainSingle();
        }
    }
}
