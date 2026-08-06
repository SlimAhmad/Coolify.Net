// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServerAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(removeServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServerAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(removeServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
