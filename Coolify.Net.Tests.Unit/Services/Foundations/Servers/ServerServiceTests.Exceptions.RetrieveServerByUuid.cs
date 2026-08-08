// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveByUuidWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByUuidWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(retrieveServerByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
