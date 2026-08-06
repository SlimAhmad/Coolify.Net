// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldRetrieveServerResourcesAsync()
        {
            // given
            string inputServerUuid = GetRandomString();
            IEnumerable<object> randomResources = new List<object> { new { Id = GetRandomString() } };

            this.serverServiceMock
                .Setup(service => service.RetrieveServerResourcesAsync(inputServerUuid))
                .ReturnsAsync(randomResources);

            // when
            IEnumerable<object> actualResources =
                await this.serverClient.RetrieveServerResourcesAsync(inputServerUuid);

            // then
            actualResources.Should().BeEquivalentTo(randomResources);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerResourcesAsync(inputServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
