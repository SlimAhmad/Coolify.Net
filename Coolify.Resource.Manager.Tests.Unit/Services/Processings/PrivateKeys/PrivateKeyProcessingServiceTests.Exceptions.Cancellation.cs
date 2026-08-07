// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync(CancellationToken.None);

            await Assert.ThrowsAsync<PrivateKeyProcessingDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllPrivateKeysTask.AsTask);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
