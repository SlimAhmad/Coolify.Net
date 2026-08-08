// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Applications;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddPublicWhenInfrastructureTimeoutOccursAsync()
        {
            Application someApplication = CreateRandomApplication();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<Application> addApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication, CancellationToken.None);

            await Assert.ThrowsAsync<ApplicationDependencyException>(addApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnAddPublicWhenCallerCancelsAsync()
        {
            Application someApplication = CreateRandomApplication();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<Application> addApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication, cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(addApplicationTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
