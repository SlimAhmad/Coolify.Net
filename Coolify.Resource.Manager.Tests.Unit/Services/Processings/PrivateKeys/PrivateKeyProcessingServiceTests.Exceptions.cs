// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions;
using Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingServiceTests
    {
        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> FoundationValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new PrivateKeyValidationException("test", inner),
                new PrivateKeyDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new PrivateKeyDependencyException("test", inner),
                new PrivateKeyServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyProcessingDependencyValidationException>(retrieveAllPrivateKeysTask.AsTask);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyProcessingDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyProcessingServiceException>(retrieveAllPrivateKeysTask.AsTask);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
