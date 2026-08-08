// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Servers;
using Coolify.Net.Models.Foundations.Servers;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveServerValidationAsync()
        {
            // given
            ExternalServer randomExternalServer = CreateRandomExternalServer();
            string inputServerUuid = randomExternalServer.Uuid;
            Server expectedServer = ConvertToServer(randomExternalServer);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetValidateServerAsync(inputServerUuid))
                .ReturnsAsync(randomExternalServer);

            // when
            Server actualServer =
                await this.serverService.RetrieveServerValidationAsync(inputServerUuid);

            // then
            actualServer.Should().BeEquivalentTo(expectedServer);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetValidateServerAsync(inputServerUuid), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
