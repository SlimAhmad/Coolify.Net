// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ServerDependencyValidationException expectedServerDependencyValidationException =
                CreateInvalidServerDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            ServerDependencyValidationException actualServerDependencyValidationException =
                await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerByUuidTask.AsTask);

            // then
            actualServerDependencyValidationException.Should()
                .BeEquivalentTo(expectedServerDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ServerDependencyValidationException expectedServerDependencyValidationException =
                CreateAlreadyExistsServerDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            ServerDependencyValidationException actualServerDependencyValidationException =
                await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerByUuidTask.AsTask);

            // then
            actualServerDependencyValidationException.Should()
                .BeEquivalentTo(expectedServerDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyValidationException))),
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
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerByUuidTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
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
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerByUuidTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerByUuidTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ServerServiceException expectedServerServiceException =
                CreateFailedServerServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerByUuidAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> retrieveServerByUuidTask =
                this.serverService.RetrieveServerByUuidAsync(someServerUuid);

            ServerServiceException actualServerServiceException =
                await Assert.ThrowsAsync<ServerServiceException>(retrieveServerByUuidTask.AsTask);

            // then
            actualServerServiceException.Should()
                .BeEquivalentTo(expectedServerServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerByUuidAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
