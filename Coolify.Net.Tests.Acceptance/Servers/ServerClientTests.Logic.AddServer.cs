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
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldAddServerAsync()
        {
            // given
            string serverUuid = GetRandomString();
            string serverName = GetRandomString();

            this.clientBroker.Server
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

            var inputServer = new Server
            {
                Name = serverName,
                Ip = "10.0.0.1",
                User = "root",
                Port = 22
            };

            // when
            Server actualServer =
                await this.clientBroker.Client.Servers.AddServerAsync(inputServer);

            // then
            actualServer.Uuid.Should().Be(serverUuid);
            actualServer.Name.Should().Be(serverName);

            var postRequest = this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/servers").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ClientBroker.ApiToken}");
        }
    }
}
