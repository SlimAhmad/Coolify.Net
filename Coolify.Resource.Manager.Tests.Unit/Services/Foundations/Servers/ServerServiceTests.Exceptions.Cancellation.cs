// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            // given
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync(CancellationToken.None);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            // given
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverService.RetrieveAllServersAsync(cancellationTokenSource.Token);

            // then
            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllServersTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddWhenInfrastructureTimeoutOccursAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(
                    It.IsAny<ExternalServer>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer, CancellationToken.None);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServerAsync(It.IsAny<ExternalServer>(), It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnAddWhenCallerCancelsAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // when
            ValueTask<Server> addServerTask =
                this.serverService.AddServerAsync(someServer, cancellationTokenSource.Token);

            // then
            await Assert.ThrowsAsync<OperationCanceledException>(addServerTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenInfrastructureTimeoutOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServerAsync(someServerUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            // when
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(someServerUuid, CancellationToken.None);

            // then
            await Assert.ThrowsAsync<ServerDependencyException>(removeServerTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServerAsync(someServerUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRemoveWhenCallerCancelsAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // when
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(someServerUuid, cancellationTokenSource.Token);

            // then
            await Assert.ThrowsAsync<OperationCanceledException>(removeServerTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
