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
        public async Task ShouldThrowDependencyValidationExceptionOnCancelDeploymentIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DeploymentDependencyValidationException expectedException =
                CreateInvalidDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> cancelDeploymentTask =
                this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(cancelDeploymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnCancelDeploymentIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DeploymentDependencyValidationException expectedException =
                CreateAlreadyExistsDeploymentDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> cancelDeploymentTask =
                this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentDependencyValidationException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyValidationException>(cancelDeploymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnCancelDeploymentIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> cancelDeploymentTask =
                this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(cancelDeploymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnCancelDeploymentIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDeploymentUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> cancelDeploymentTask =
                this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(cancelDeploymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnCancelDeploymentIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DeploymentDependencyException expectedException =
                CreateFailedDeploymentDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Deployment> cancelDeploymentTask =
                this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentDependencyException actualException =
                await Assert.ThrowsAsync<DeploymentDependencyException>(cancelDeploymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnCancelDeploymentIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDeploymentUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DeploymentServiceException expectedException =
                CreateFailedDeploymentServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(someDeploymentUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Deployment> cancelDeploymentTask =
                this.deploymentService.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentServiceException actualException =
                await Assert.ThrowsAsync<DeploymentServiceException>(cancelDeploymentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(someDeploymentUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
