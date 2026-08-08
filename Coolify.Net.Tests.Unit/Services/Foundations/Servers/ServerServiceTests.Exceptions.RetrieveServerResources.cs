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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveResourcesWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerResourcesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveResourcesWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerResourcesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveResourcesWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerResourcesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveResourcesWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerResourcesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveResourcesWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(retrieveServerResourcesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
