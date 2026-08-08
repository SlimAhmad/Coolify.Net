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
        public async Task ShouldModifyServerAsync()
        {
            // given
            string serverUuid = GetRandomString();
            string modifiedName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = serverUuid,
                        name = modifiedName,
                        ip = "10.0.0.1",
                        user = "root",
                        port = 22
                    }));

            var inputServer = new Server
            {
                Uuid = serverUuid,
                Name = modifiedName,
                Ip = "10.0.0.1",
                User = "root",
                Port = 22
            };

            // when
            Server actualServer =
                await this.clientBroker.Client.Servers.ModifyServerAsync(inputServer);

            // then
            actualServer.Uuid.Should().Be(serverUuid);
            actualServer.Name.Should().Be(modifiedName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/servers/{serverUuid}").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
