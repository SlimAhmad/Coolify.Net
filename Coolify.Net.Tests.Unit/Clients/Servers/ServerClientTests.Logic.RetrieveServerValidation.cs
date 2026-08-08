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
        public async Task ShouldRetrieveServerValidationAsync()
        {
            // given
            Server randomServer = CreateRandomServer();
            string inputServerUuid = randomServer.Uuid;

            this.serverServiceMock
                .Setup(service => service.RetrieveServerValidationAsync(inputServerUuid))
                .ReturnsAsync(randomServer);

            // when
            Server actualServer =
                await this.serverClient.RetrieveServerValidationAsync(inputServerUuid);

            // then
            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerValidationAsync(inputServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
