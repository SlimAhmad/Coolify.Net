// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Systems;
using Coolify.Resource.Manager.Models.Foundations.Systems.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Systems
{
    public partial class SystemServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveVersionWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<SystemInfo> retrieveVersionTask =
                this.systemService.RetrieveVersionAsync(CancellationToken.None);

            await Assert.ThrowsAsync<SystemDependencyException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveVersionWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<SystemInfo> retrieveVersionTask =
                this.systemService.RetrieveVersionAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
