// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Models.Processings.Deployments.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Deployments
{
    public partial class DeploymentProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentProcessingService.RetrieveAllDeploymentsAsync(CancellationToken.None);

            await Assert.ThrowsAsync<DeploymentProcessingDependencyException>(retrieveAllDeploymentsTask.AsTask);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentProcessingService.RetrieveAllDeploymentsAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllDeploymentsTask.AsTask);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
