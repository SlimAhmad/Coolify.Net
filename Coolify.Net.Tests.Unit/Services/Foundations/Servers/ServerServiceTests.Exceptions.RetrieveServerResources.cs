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
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveResourcesIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ServerDependencyValidationException expectedServerDependencyValidationException =
                CreateInvalidServerDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            ServerDependencyValidationException actualServerDependencyValidationException =
                await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerResourcesTask.AsTask);

            // then
            actualServerDependencyValidationException.Should()
                .BeEquivalentTo(expectedServerDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveResourcesIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ServerDependencyValidationException expectedServerDependencyValidationException =
                CreateAlreadyExistsServerDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            ServerDependencyValidationException actualServerDependencyValidationException =
                await Assert.ThrowsAsync<ServerDependencyValidationException>(retrieveServerResourcesTask.AsTask);

            // then
            actualServerDependencyValidationException.Should()
                .BeEquivalentTo(expectedServerDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveResourcesIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerResourcesTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveResourcesIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServerUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerResourcesTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveResourcesIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(retrieveServerResourcesTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveResourcesIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ServerServiceException expectedServerServiceException =
                CreateFailedServerServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServerResourcesAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<object>> retrieveServerResourcesTask =
                this.serverService.RetrieveServerResourcesAsync(someServerUuid);

            ServerServiceException actualServerServiceException =
                await Assert.ThrowsAsync<ServerServiceException>(retrieveServerResourcesTask.AsTask);

            // then
            actualServerServiceException.Should()
                .BeEquivalentTo(expectedServerServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServerResourcesAsync(someServerUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
