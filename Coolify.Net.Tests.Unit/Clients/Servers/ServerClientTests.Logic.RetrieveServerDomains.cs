// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldRetrieveServerDomainsAsync()
        {
            // given
            string inputServerUuid = GetRandomString();
            IEnumerable<string> randomDomains = new List<string> { GetRandomString(), GetRandomString() };

            this.serverServiceMock
                .Setup(service => service.RetrieveServerDomainsAsync(inputServerUuid))
                .ReturnsAsync(randomDomains);

            // when
            IEnumerable<string> actualDomains =
                await this.serverClient.RetrieveServerDomainsAsync(inputServerUuid);

            // then
            actualDomains.Should().BeEquivalentTo(randomDomains);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerDomainsAsync(inputServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
