// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddPrivateKeyIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            PrivateKeyDependencyValidationException expectedException =
                CreateInvalidPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(addPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddPrivateKeyIfConflictErrorOccursAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            PrivateKeyDependencyValidationException expectedException =
                CreateAlreadyExistsPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(addPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddPrivateKeyIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(addPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddPrivateKeyIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(addPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddPrivateKeyIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var httpRequestException = new HttpRequestException("Network failure.");

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(addPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddPrivateKeyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var exception = new Exception("Unexpected error.");

            PrivateKeyServiceException expectedException =
                CreateFailedPrivateKeyServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<PrivateKey> addPrivateKeyTask =
                this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            PrivateKeyServiceException actualException =
                await Assert.ThrowsAsync<PrivateKeyServiceException>(addPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
