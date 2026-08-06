// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveServerDomainsAsync()
        {
            // given
            string inputServerUuid = GetRandomString();
            IEnumerable<string> expectedDomains = new List<string> { GetRandomString(), GetRandomString() };

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerDomainsAsync(inputServerUuid))
                .ReturnsAsync(expectedDomains);

            // when
            IEnumerable<string> actualDomains =
                await this.serverService.RetrieveServerDomainsAsync(inputServerUuid);

            // then
            actualDomains.Should().BeEquivalentTo(expectedDomains);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerDomainsAsync(inputServerUuid), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
