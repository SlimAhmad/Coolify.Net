// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldAddServerAsync()
        {
            // given
            Server inputServer = CreateRandomServer();
            Server randomServer = CreateRandomServer();

            this.serverServiceMock
                .Setup(service => service.AddServerAsync(inputServer))
                .ReturnsAsync(randomServer);

            // when
            Server actualServer =
                await this.serverClient.AddServerAsync(inputServer);

            // then
            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(
                service => service.AddServerAsync(inputServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
