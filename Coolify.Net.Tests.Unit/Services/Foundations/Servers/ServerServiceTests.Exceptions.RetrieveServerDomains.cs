// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveDomainsWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerDomainsAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverService.RetrieveServerDomainsAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerDomainsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerDomainsAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveDomainsWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerDomainsAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverService.RetrieveServerDomainsAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerDomainsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerDomainsAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveDomainsWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerDomainsAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverService.RetrieveServerDomainsAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerDomainsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerDomainsAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveDomainsWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerDomainsAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverService.RetrieveServerDomainsAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerDomainsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerDomainsAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveDomainsWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerDomainsAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverService.RetrieveServerDomainsAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(retrieveServerDomainsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerDomainsAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
