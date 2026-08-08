// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrievePrivateKeyByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            PrivateKeyDependencyValidationException expectedException =
                CreateInvalidPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> retrievePrivateKeyByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(somePrivateKeyUuid);

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(retrievePrivateKeyByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrievePrivateKeyByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            PrivateKeyDependencyValidationException expectedException =
                CreateAlreadyExistsPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> retrievePrivateKeyByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(somePrivateKeyUuid);

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(retrievePrivateKeyByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrievePrivateKeyByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> retrievePrivateKeyByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(somePrivateKeyUuid);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrievePrivateKeyByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrievePrivateKeyByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> retrievePrivateKeyByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(somePrivateKeyUuid);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrievePrivateKeyByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrievePrivateKeyByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> retrievePrivateKeyByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(somePrivateKeyUuid);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrievePrivateKeyByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrievePrivateKeyByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            PrivateKeyServiceException expectedException =
                CreateFailedPrivateKeyServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<PrivateKey> retrievePrivateKeyByUuidTask =
                this.privateKeyService.RetrievePrivateKeyByUuidAsync(somePrivateKeyUuid);

            PrivateKeyServiceException actualException =
                await Assert.ThrowsAsync<PrivateKeyServiceException>(retrievePrivateKeyByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
