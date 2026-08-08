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
        public async Task ShouldThrowDependencyValidationExceptionOnDeployByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DeploymentDependencyValidationException expectedException =
                CreateInvalidDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> deployByUuidTask =
                this.deploymentService.DeployByUuidAsync(someUuid);

            DeploymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(deployByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnDeployByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DeploymentDependencyValidationException expectedException =
                CreateAlreadyExistsDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> deployByUuidTask =
                this.deploymentService.DeployByUuidAsync(someUuid);

            DeploymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(deployByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnDeployByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> deployByUuidTask =
                this.deploymentService.DeployByUuidAsync(someUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(deployByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnDeployByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> deployByUuidTask =
                this.deploymentService.DeployByUuidAsync(someUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(deployByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnDeployByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> deployByUuidTask =
                this.deploymentService.DeployByUuidAsync(someUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(deployByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnDeployByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DeploymentServiceException expectedException =
                CreateFailedDeploymentServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(someUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Deployment> deployByUuidTask =
                this.deploymentService.DeployByUuidAsync(someUuid);

            DeploymentServiceException actualException =
                await Assert.ThrowsAsync<DeploymentServiceException>(deployByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(someUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
