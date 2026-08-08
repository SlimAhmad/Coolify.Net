// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
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
        public async Task ShouldThrowDependencyExceptionOnRemoveIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServerAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(removeServerTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ServerServiceException expectedServerServiceException =
                CreateFailedServerServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServerAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(someServerUuid);

            ServerServiceException actualServerServiceException =
                await Assert.ThrowsAsync<ServerServiceException>(removeServerTask.AsTask);

            // then
            actualServerServiceException.Should()
                .BeEquivalentTo(expectedServerServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteServerAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
