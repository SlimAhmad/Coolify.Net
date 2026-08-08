// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Processings.Applications.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Applications
{
    public partial class ApplicationProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationProcessingService.RetrieveAllApplicationsAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ApplicationProcessingDependencyException>(retrieveAllApplicationsTask.AsTask);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationProcessingService.RetrieveAllApplicationsAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllApplicationsTask.AsTask);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
