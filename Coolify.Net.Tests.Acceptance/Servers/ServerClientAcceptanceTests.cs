// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Servers
{
    public class ServerClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public ServerClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldProvisionAndDeprovisionServerAsync()
        {
            // given
            string serverUuid = Guid.NewGuid().ToString();
            string serverName = "acceptance-server";

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/servers").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = serverUuid,
                        name = serverName,
                        ip = "10.0.0.1",
                        user = "root",
                        port = 22,
                        is_reachable = false,
                        is_usable = false
                    }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = serverUuid,
                        name = serverName,
                        ip = "10.0.0.1",
                        user = "root",
                        port = 22,
                        is_reachable = true,
                        is_usable = true
                    }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            var newServer = new Server
            {
                Name = serverName,
                Ip = "10.0.0.1",
                User = "root",
                Port = 22
            };

            // when
            Server addedServer =
                await this.apiFixture.Client.Servers.AddServerAsync(newServer);

            Server retrievedServer =
                await this.apiFixture.Client.Servers.RetrieveServerByUuidAsync(serverUuid);

            await this.apiFixture.Client.Servers.RemoveServerAsync(serverUuid);

            // then
            addedServer.Uuid.Should().Be(serverUuid);
            addedServer.Name.Should().Be(serverName);
            retrievedServer.IsReachable.Should().BeTrue();
            retrievedServer.IsUsable.Should().BeTrue();

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/servers").UsingPost())
                .Should().ContainSingle();

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingDelete())
                .Should().ContainSingle();

            var postRequest = this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/servers").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ApiFixture.ApiToken}");
        }

        [Fact]
        public async Task ShouldRetrieveAllServersAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/servers").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "server-one" },
                        new { uuid = Guid.NewGuid().ToString(), name = "server-two" }
                    }));

            // when
            IEnumerable<Server> actualServers =
                await this.apiFixture.Client.Servers.RetrieveAllServersAsync();

            // then
            actualServers.Should().HaveCount(2);
        }
    }
}
