// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenPrivateKeyIsNullAndLogItAsync()
        {
            PrivateKey nullPrivateKey = null;

            var nullPrivateKeyProcessingException =
                new NullPrivateKeyProcessingException(message: "Private key is null.");

            var expectedPrivateKeyProcessingValidationException =
                new PrivateKeyProcessingValidationException(
                    message: "Private key processing validation error occurred, fix the errors and try again.",
                    innerException: nullPrivateKeyProcessingException);

            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyProcessingService.AddPrivateKeyAsync(nullPrivateKey);

            PrivateKeyProcessingValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyProcessingValidationException>(addPrivateKeyTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenPrivateKeyUuidIsInvalidAndLogItAsync(
            string invalidPrivateKeyUuid)
        {
            var invalidPrivateKeyProcessingException =
                new InvalidPrivateKeyProcessingException(message: "Private key uuid is invalid.");

            var expectedPrivateKeyProcessingValidationException =
                new PrivateKeyProcessingValidationException(
                    message: "Private key processing validation error occurred, fix the errors and try again.",
                    innerException: invalidPrivateKeyProcessingException);

            ValueTask<PrivateKey> retrieveByUuidTask =
                this.privateKeyProcessingService.RetrievePrivateKeyByUuidAsync(invalidPrivateKeyUuid);

            PrivateKeyProcessingValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenPrivateKeyUuidIsInvalidAndLogItAsync(
            string invalidPrivateKeyUuid)
        {
            var invalidPrivateKeyProcessingException =
                new InvalidPrivateKeyProcessingException(message: "Private key uuid is invalid.");

            var expectedPrivateKeyProcessingValidationException =
                new PrivateKeyProcessingValidationException(
                    message: "Private key processing validation error occurred, fix the errors and try again.",
                    innerException: invalidPrivateKeyProcessingException);

            ValueTask removePrivateKeyTask =
                this.privateKeyProcessingService.RemovePrivateKeyAsync(invalidPrivateKeyUuid);

            PrivateKeyProcessingValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyProcessingValidationException>(removePrivateKeyTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
