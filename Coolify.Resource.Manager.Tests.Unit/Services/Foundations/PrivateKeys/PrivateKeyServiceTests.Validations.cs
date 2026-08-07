// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenPrivateKeyIsNullAndLogItAsync()
        {
            PrivateKey nullPrivateKey = null;

            var nullPrivateKeyException = new NullPrivateKeyException(message: "Private key is null.");

            var expectedPrivateKeyValidationException =
                new PrivateKeyValidationException(
                    message: "Private key validation error occurred, fix the errors and try again.",
                    innerException: nullPrivateKeyException);

            ValueTask<PrivateKey> addPrivateKeyTask = this.privateKeyService.AddPrivateKeyAsync(nullPrivateKey);

            PrivateKeyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyValidationException>(addPrivateKeyTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddWhenPrivateKeyIsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidPrivateKey = new PrivateKey
            {
                Name = invalidText,
                PrivateKeyValue = invalidText
            };

            var invalidPrivateKeyException =
                new InvalidPrivateKeyException(
                    message: "Invalid private key. Please fix the errors and try again.");

            invalidPrivateKeyException.UpsertDataList(key: nameof(PrivateKey.Name), value: "Text is required");
            invalidPrivateKeyException.UpsertDataList(key: nameof(PrivateKey.PrivateKeyValue), value: "Text is required");

            var expectedPrivateKeyValidationException =
                new PrivateKeyValidationException(
                    message: "Private key validation error occurred, fix the errors and try again.",
                    innerException: invalidPrivateKeyException);

            ValueTask<PrivateKey> addPrivateKeyTask = this.privateKeyService.AddPrivateKeyAsync(invalidPrivateKey);

            PrivateKeyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyValidationException>(addPrivateKeyTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenPrivateKeyUuidIsInvalidAndLogItAsync(
            string invalidPrivateKeyUuid)
        {
            var invalidPrivateKeyException =
                new InvalidPrivateKeyException(
                    message: "Invalid private key. Please fix the errors and try again.");

            invalidPrivateKeyException.UpsertDataList(key: "privateKeyUuid", value: "Text is required");

            var expectedPrivateKeyValidationException =
                new PrivateKeyValidationException(
                    message: "Private key validation error occurred, fix the errors and try again.",
                    innerException: invalidPrivateKeyException);

            ValueTask<PrivateKey> retrieveByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(invalidPrivateKeyUuid);

            PrivateKeyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenPrivateKeyUuidIsInvalidAndLogItAsync(
            string invalidPrivateKeyUuid)
        {
            var invalidPrivateKeyException =
                new InvalidPrivateKeyException(
                    message: "Invalid private key. Please fix the errors and try again.");

            invalidPrivateKeyException.UpsertDataList(key: "privateKeyUuid", value: "Text is required");

            var expectedPrivateKeyValidationException =
                new PrivateKeyValidationException(
                    message: "Private key validation error occurred, fix the errors and try again.",
                    innerException: invalidPrivateKeyException);

            ValueTask removePrivateKeyTask =
                this.privateKeyService.RemovePrivateKeyAsync(invalidPrivateKeyUuid);

            PrivateKeyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyValidationException>(removePrivateKeyTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedPrivateKeyValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
