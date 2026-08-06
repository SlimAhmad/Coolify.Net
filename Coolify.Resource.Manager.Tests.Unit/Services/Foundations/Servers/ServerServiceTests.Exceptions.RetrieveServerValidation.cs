// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveValidationWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetValidateServerAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverService.RetrieveServerValidationAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerValidationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetValidateServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveValidationWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetValidateServerAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverService.RetrieveServerValidationAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerValidationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetValidateServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveValidationWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetValidateServerAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverService.RetrieveServerValidationAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerValidationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetValidateServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveValidationWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetValidateServerAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverService.RetrieveServerValidationAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerValidationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetValidateServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveValidationWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetValidateServerAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverService.RetrieveServerValidationAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(retrieveServerValidationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetValidateServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
