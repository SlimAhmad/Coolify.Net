// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Externals.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyValidationException>(modifyServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(modifyServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnModifyWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            // given
            Server someServer = CreateRandomServer();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(modifyServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(modifyServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyWhenExceptionOccursAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerServiceException>(modifyServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchServerAsync(It.IsAny<ExternalServer>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
