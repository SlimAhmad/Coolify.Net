// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveServerResourcesAsync()
        {
            // given
            string inputServerUuid = GetRandomString();
            IEnumerable<object> expectedResources = new List<object> { new { Id = GetRandomString() } };

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(inputServerUuid))
                .ReturnsAsync(expectedResources);

            // when
            IEnumerable<object> actualResources =
                await this.serverService.RetrieveServerResourcesAsync(inputServerUuid);

            // then
            actualResources.Should().BeEquivalentTo(expectedResources);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(inputServerUuid), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
