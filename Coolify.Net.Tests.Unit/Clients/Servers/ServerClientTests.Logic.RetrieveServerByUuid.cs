// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldRetrieveServerByUuidAsync()
        {
            // given
            Server randomServer = CreateRandomServer();
            string inputServerUuid = randomServer.Uuid;

            this.serverServiceMock
                .Setup(service => service.RetrieveServerByUuidAsync(inputServerUuid))
                .ReturnsAsync(randomServer);

            // when
            Server actualServer =
                await this.serverClient.RetrieveServerByUuidAsync(inputServerUuid);

            // then
            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerByUuidAsync(inputServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
