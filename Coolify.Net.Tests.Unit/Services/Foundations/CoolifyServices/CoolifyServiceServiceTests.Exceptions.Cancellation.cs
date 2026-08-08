// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync(CancellationToken.None);

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddWhenInfrastructureTimeoutOccursAsync()
        {
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceService.AddCoolifyServiceAsync(someCoolifyService, CancellationToken.None);

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(addCoolifyServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnAddWhenCallerCancelsAsync()
        {
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceService.AddCoolifyServiceAsync(someCoolifyService, cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(addCoolifyServiceTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
