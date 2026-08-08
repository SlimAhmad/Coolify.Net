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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DeploymentDependencyValidationException expectedDeploymentDependencyValidationException =
                CreateInvalidDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> retrieveDeploymentByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(someDeploymentUuid);

            DeploymentDependencyValidationException actualDeploymentDependencyValidationException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveDeploymentByUuidTask.AsTask);

            // then
            actualDeploymentDependencyValidationException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DeploymentDependencyValidationException expectedDeploymentDependencyValidationException =
                CreateAlreadyExistsDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> retrieveDeploymentByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(someDeploymentUuid);

            DeploymentDependencyValidationException actualDeploymentDependencyValidationException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(retrieveDeploymentByUuidTask.AsTask);

            // then
            actualDeploymentDependencyValidationException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedDeploymentDependencyException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> retrieveDeploymentByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(someDeploymentUuid);

            DeploymentDependencyException actualDeploymentDependencyException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveDeploymentByUuidTask.AsTask);

            // then
            actualDeploymentDependencyException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedDeploymentDependencyException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> retrieveDeploymentByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(someDeploymentUuid);

            DeploymentDependencyException actualDeploymentDependencyException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveDeploymentByUuidTask.AsTask);

            // then
            actualDeploymentDependencyException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DeploymentDependencyException expectedDeploymentDependencyException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> retrieveDeploymentByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(someDeploymentUuid);

            DeploymentDependencyException actualDeploymentDependencyException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(retrieveDeploymentByUuidTask.AsTask);

            // then
            actualDeploymentDependencyException.Should()
                .BeEquivalentTo(expectedDeploymentDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedDeploymentDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DeploymentServiceException expectedDeploymentServiceException =
                CreateFailedDeploymentServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<Deployment> retrieveDeploymentByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(someDeploymentUuid);

            DeploymentServiceException actualDeploymentServiceException =
                await Assert.ThrowsAsync<DeploymentServiceException>(retrieveDeploymentByUuidTask.AsTask);

            // then
            actualDeploymentServiceException.Should()
                .BeEquivalentTo(expectedDeploymentServiceException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedDeploymentServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
