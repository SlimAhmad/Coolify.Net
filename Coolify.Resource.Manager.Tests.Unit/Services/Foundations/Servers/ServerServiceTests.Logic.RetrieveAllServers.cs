// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllServersAsync()
        {
            // given
            List<ExternalServer> randomExternalServers =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalServer()).ToList();

            IEnumerable<Server> expectedServers =
                randomExternalServers.Select(ConvertToServer);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync())
                .ReturnsAsync(randomExternalServers);

            // when
            IEnumerable<Server> actualServers =
                await this.serverService.RetrieveAllServersAsync();

            // then
            actualServers.Should().BeEquivalentTo(expectedServers);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
