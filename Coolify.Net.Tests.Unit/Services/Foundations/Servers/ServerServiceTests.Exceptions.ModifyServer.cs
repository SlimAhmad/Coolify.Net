// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Servers;
using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ServerDependencyValidationException expectedServerDependencyValidationException =
                CreateInvalidServerDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            ServerDependencyValidationException actualServerDependencyValidationException =
                await Assert.ThrowsAsync<ServerDependencyValidationException>(modifyServerTask.AsTask);

            // then
            actualServerDependencyValidationException.Should()
                .BeEquivalentTo(expectedServerDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfConflictErrorOccursAndLogItAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ServerDependencyValidationException expectedServerDependencyValidationException =
                CreateAlreadyExistsServerDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            ServerDependencyValidationException actualServerDependencyValidationException =
                await Assert.ThrowsAsync<ServerDependencyValidationException>(modifyServerTask.AsTask);

            // then
            actualServerDependencyValidationException.Should()
                .BeEquivalentTo(expectedServerDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(modifyServerTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnModifyIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(modifyServerTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var httpRequestException = new HttpRequestException("Network failure.");

            ServerDependencyException expectedServerDependencyException =
                CreateFailedServerDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            ServerDependencyException actualServerDependencyException =
                await Assert.ThrowsAsync<ServerDependencyException>(modifyServerTask.AsTask);

            // then
            actualServerDependencyException.Should()
                .BeEquivalentTo(expectedServerDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedServerDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var exception = new Exception("Unexpected error.");

            ServerServiceException expectedServerServiceException =
                CreateFailedServerServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            ServerServiceException actualServerServiceException =
                await Assert.ThrowsAsync<ServerServiceException>(modifyServerTask.AsTask);

            // then
            actualServerServiceException.Should()
                .BeEquivalentTo(expectedServerServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedServerServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
