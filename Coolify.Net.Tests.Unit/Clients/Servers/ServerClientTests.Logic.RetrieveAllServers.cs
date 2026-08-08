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
        public async Task ShouldRetrieveAllServersAsync()
        {
            // given
            IEnumerable<Server> randomServers =
                Enumerable.Range(0, 3).Select(_ => CreateRandomServer());

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync())
                .ReturnsAsync(randomServers);

            // when
            IEnumerable<Server> actualServers =
                await this.serverClient.RetrieveAllServersAsync();

            // then
            actualServers.Should().BeEquivalentTo(randomServers);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
