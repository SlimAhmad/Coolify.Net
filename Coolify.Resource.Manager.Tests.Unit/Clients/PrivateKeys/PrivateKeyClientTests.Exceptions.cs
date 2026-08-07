// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.PrivateKeys.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.PrivateKeys
{
    public partial class PrivateKeyClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new PrivateKeyClientValidationException(
                message: "Private key client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<PrivateKey>> task = this.privateKeyClient.RetrieveAllPrivateKeysAsync();

            PrivateKeyClientValidationException actual =
                await Assert.ThrowsAsync<PrivateKeyClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new PrivateKeyClientDependencyException(
                message: "Private key client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<PrivateKey>> task = this.privateKeyClient.RetrieveAllPrivateKeysAsync();

            PrivateKeyClientDependencyException actual =
                await Assert.ThrowsAsync<PrivateKeyClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<PrivateKey>> task = this.privateKeyClient.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyClientServiceException>(task.AsTask);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<PrivateKey>> task = this.privateKeyClient.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnAddWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            PrivateKey somePrivateKey = CreateRandomPrivateKey();

            var expected = new PrivateKeyClientValidationException(
                message: "Private key client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.privateKeyServiceMock
                .Setup(service => service.AddPrivateKeyAsync(somePrivateKey, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<PrivateKey> task = this.privateKeyClient.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyClientValidationException actual =
                await Assert.ThrowsAsync<PrivateKeyClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.privateKeyServiceMock.Verify(service =>
                service.AddPrivateKeyAsync(somePrivateKey, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var exception = new Exception("Unexpected error.");

            this.privateKeyServiceMock
                .Setup(service => service.AddPrivateKeyAsync(somePrivateKey, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<PrivateKey> task = this.privateKeyClient.AddPrivateKeyAsync(somePrivateKey);

            await Assert.ThrowsAsync<PrivateKeyClientServiceException>(task.AsTask);

            this.privateKeyServiceMock.Verify(service =>
                service.AddPrivateKeyAsync(somePrivateKey, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRemoveWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string somePrivateKeyUuid = GetRandomString();

            var expected = new PrivateKeyClientValidationException(
                message: "Private key client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.privateKeyServiceMock
                .Setup(service => service.RemovePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.privateKeyClient.RemovePrivateKeyAsync(somePrivateKeyUuid);

            PrivateKeyClientValidationException actual =
                await Assert.ThrowsAsync<PrivateKeyClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.privateKeyServiceMock.Verify(service =>
                service.RemovePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string somePrivateKeyUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.privateKeyServiceMock
                .Setup(service => service.RemovePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.privateKeyClient.RemovePrivateKeyAsync(somePrivateKeyUuid);

            await Assert.ThrowsAsync<PrivateKeyClientServiceException>(task.AsTask);

            this.privateKeyServiceMock.Verify(service =>
                service.RemovePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
        }
    }
}
