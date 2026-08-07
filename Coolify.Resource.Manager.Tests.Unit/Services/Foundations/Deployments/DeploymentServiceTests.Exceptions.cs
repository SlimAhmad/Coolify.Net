// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentServiceException>(retrieveAllDeploymentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnDeployByUuidWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            string someUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<Deployment> deployTask = this.deploymentService.DeployByUuidAsync(someUuid);

            await Assert.ThrowsAsync<DeploymentDependencyException>(deployTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnDeployByUuidWhenExceptionOccursAsync()
        {
            string someUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Deployment> deployTask = this.deploymentService.DeployByUuidAsync(someUuid);

            await Assert.ThrowsAsync<DeploymentServiceException>(deployTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnCancelWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<Deployment> cancelTask = this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            await Assert.ThrowsAsync<DeploymentDependencyException>(cancelTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnCancelWhenExceptionOccursAsync()
        {
            string someDeploymentUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Deployment> cancelTask = this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            await Assert.ThrowsAsync<DeploymentServiceException>(cancelTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
