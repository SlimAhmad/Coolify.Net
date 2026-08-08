// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Servers;
using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnAddWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnAddWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
