// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Models.Foundations.Deployments.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveApplicationDeploymentsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DeploymentDependencyValidationException expectedException =
                CreateInvalidDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveApplicationDeploymentsTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(someApplicationUuid);

            DeploymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveApplicationDeploymentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveApplicationDeploymentsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DeploymentDependencyValidationException expectedException =
                CreateAlreadyExistsDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveApplicationDeploymentsTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(someApplicationUuid);

            DeploymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveApplicationDeploymentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveApplicationDeploymentsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveApplicationDeploymentsTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(someApplicationUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveApplicationDeploymentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveApplicationDeploymentsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveApplicationDeploymentsTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(someApplicationUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveApplicationDeploymentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveApplicationDeploymentsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveApplicationDeploymentsTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(someApplicationUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveApplicationDeploymentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveApplicationDeploymentsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DeploymentServiceException expectedException =
                CreateFailedDeploymentServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(someApplicationUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveApplicationDeploymentsTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(someApplicationUuid);

            DeploymentServiceException actualException =
                await Assert.ThrowsAsync<DeploymentServiceException>(retrieveApplicationDeploymentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
