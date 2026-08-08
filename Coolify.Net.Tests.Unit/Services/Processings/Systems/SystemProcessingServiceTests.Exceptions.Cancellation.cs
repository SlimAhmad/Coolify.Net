// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems;
using Coolify.Net.Models.Processings.Systems.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Systems
{
    public partial class SystemProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveVersionWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<SystemInfo> retrieveVersionTask =
                this.systemProcessingService.RetrieveVersionAsync(CancellationToken.None);

            await Assert.ThrowsAsync<SystemProcessingDependencyException>(retrieveVersionTask.AsTask);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveVersionWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<SystemInfo> retrieveVersionTask =
                this.systemProcessingService.RetrieveVersionAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveVersionTask.AsTask);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
