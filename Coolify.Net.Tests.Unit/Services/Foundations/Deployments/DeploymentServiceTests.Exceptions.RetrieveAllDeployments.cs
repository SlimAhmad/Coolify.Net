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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DeploymentDependencyValidationException expectedDeploymentDependencyValidationException =
                CreateInvalidDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            DeploymentDependencyValidationException actualDeploymentDependencyValidationException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveAllDeploymentsTask.AsTask);

            // then
            actualDeploymentDependencyValidationException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfConflictErrorOccursAndLogItAsync()
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DeploymentDependencyValidationException expectedDeploymentDependencyValidationException =
                CreateAlreadyExistsDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            DeploymentDependencyValidationException actualDeploymentDependencyValidationException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveAllDeploymentsTask.AsTask);

            // then
            actualDeploymentDependencyValidationException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedDeploymentDependencyException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            DeploymentDependencyException actualDeploymentDependencyException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            // then
            actualDeploymentDependencyException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedDeploymentDependencyException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            DeploymentDependencyException actualDeploymentDependencyException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            // then
            actualDeploymentDependencyException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var httpRequestException = new HttpRequestException("Network failure.");

            DeploymentDependencyException expectedDeploymentDependencyException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            DeploymentDependencyException actualDeploymentDependencyException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveAllDeploymentsTask.AsTask);

            // then
            actualDeploymentDependencyException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var exception = new Exception("Unexpected error.");

            DeploymentServiceException expectedDeploymentServiceException =
                CreateFailedDeploymentServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentService.RetrieveAllDeploymentsAsync();

            DeploymentServiceException actualDeploymentServiceException =
                await Assert.ThrowsAsync<DeploymentServiceException>(retrieveAllDeploymentsTask.AsTask);

            // then
            actualDeploymentServiceException.Should()
                .BeEquivalentTo(expectedDeploymentServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
