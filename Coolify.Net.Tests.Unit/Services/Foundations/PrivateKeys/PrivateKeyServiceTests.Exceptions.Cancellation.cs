// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync(CancellationToken.None);

            await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddWhenInfrastructureTimeoutOccursAsync()
        {
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(
                    It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey, CancellationToken.None);

            await Assert.ThrowsAsync<PrivateKeyDependencyException>(addPrivateKeyTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnAddWhenCallerCancelsAsync()
        {
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey, cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(addPrivateKeyTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
