// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync(CancellationToken.None);

            await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

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

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnDeployByUuidWhenInfrastructureTimeoutOccursAsync()
        {
            string someUuid = GetRandomString();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<Deployment> deployTask =
                this.deploymentService.DeployByUuidAsync(someUuid, CancellationToken.None);

            await Assert.ThrowsAsync<DeploymentDependencyException>(deployTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnDeployByUuidWhenCallerCancelsAsync()
        {
            string someUuid = GetRandomString();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<Deployment> deployTask =
                this.deploymentService.DeployByUuidAsync(someUuid, cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(deployTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
