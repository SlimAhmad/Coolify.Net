// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Moq;

namespace Coolify.Net.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldRemoveServerAsync()
        {
            // given
            string inputServerUuid = GetRandomString();

            this.serverServiceMock
                .Setup(service => service.RemoveServerAsync(inputServerUuid))
                .Returns(ValueTask.CompletedTask);

            // when
            await this.serverClient.RemoveServerAsync(inputServerUuid);

            // then
            this.serverServiceMock.Verify(
                service => service.RemoveServerAsync(inputServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
