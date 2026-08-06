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
        public async Task ShouldModifyServerAsync()
        {
            // given
            Server inputServer = CreateRandomServer();
            ExternalServer inputExternalServer = ConvertToExternalServer(inputServer);
            ExternalServer returnedExternalServer = CreateRandomExternalServer();
            Server expectedServer = ConvertToServer(returnedExternalServer);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(
                    It.Is<ExternalServer>(external => IsSameExternalServer(external, inputExternalServer))))
                .ReturnsAsync(returnedExternalServer);

            // when
            Server actualServer =
                await this.serverService.ModifyServerAsync(inputServer);

            // then
            actualServer.Should().BeEquivalentTo(expectedServer);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServerAsync(
                    It.Is<ExternalServer>(external => IsSameExternalServer(external, inputExternalServer))),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
