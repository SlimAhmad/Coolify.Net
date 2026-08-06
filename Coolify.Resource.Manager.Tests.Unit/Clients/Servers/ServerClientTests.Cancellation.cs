// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Fact]
        public async Task ShouldForwardCancellationTokenOnRetrieveAllServersAsync()
        {
            // given
            using var cancellationTokenSource = new CancellationTokenSource();
            IEnumerable<Server> randomServers = Enumerable.Range(0, 3).Select(_ => CreateRandomServer());

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(cancellationTokenSource.Token))
                .ReturnsAsync(randomServers);

            // when
            await this.serverClient.RetrieveAllServersAsync(cancellationTokenSource.Token);

            // then
            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(cancellationTokenSource.Token), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllServersAsync()
        {
            // given
            var operationCanceledException = new OperationCanceledException();

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverClient.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
