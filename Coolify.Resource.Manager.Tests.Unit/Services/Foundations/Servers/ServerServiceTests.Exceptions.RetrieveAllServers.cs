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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(), Times.Once);

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
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(), Times.Once);

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
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            // given
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<ServerServiceException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
