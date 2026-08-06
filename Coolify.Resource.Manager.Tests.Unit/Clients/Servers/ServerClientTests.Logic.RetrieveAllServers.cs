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
