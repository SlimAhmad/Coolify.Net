// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldRemoveServerAsync()
        {
            // given
            string inputServerUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServerAsync(inputServerUuid))
                .Returns(ValueTask.CompletedTask);

            // when
            await this.serverService.RemoveServerAsync(inputServerUuid);

            // then
            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteServerAsync(inputServerUuid), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
