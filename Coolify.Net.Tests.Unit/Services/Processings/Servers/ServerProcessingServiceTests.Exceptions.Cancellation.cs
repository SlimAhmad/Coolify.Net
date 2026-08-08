// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Processings.Servers.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Servers
{
    public partial class ServerProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverProcessingService.RetrieveAllServersAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ServerProcessingDependencyException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverProcessingService.RetrieveAllServersAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
