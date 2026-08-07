// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Externals.PrivateKeys;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            await Assert.ThrowsAsync<PrivateKeyServiceException>(retrieveAllPrivateKeysTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnAddWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(
                    It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<PrivateKey> addPrivateKeyTask = this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

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
        public async Task ShouldThrowServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(
                    It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<PrivateKey> addPrivateKeyTask = this.privateKeyService.AddPrivateKeyAsync(somePrivateKey);

            await Assert.ThrowsAsync<PrivateKeyServiceException>(addPrivateKeyTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            string somePrivateKeyUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeletePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask removePrivateKeyTask = this.privateKeyService.RemovePrivateKeyAsync(somePrivateKeyUuid);

            await Assert.ThrowsAsync<PrivateKeyDependencyException>(removePrivateKeyTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeletePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string somePrivateKeyUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeletePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask removePrivateKeyTask = this.privateKeyService.RemovePrivateKeyAsync(somePrivateKeyUuid);

            await Assert.ThrowsAsync<PrivateKeyServiceException>(removePrivateKeyTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeletePrivateKeyAsync(somePrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
