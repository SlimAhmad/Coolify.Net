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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyPrivateKeyIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            PrivateKeyDependencyValidationException expectedException =
                CreateInvalidPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> modifyPrivateKeyTask =
                this.privateKeyService.ModifyPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(modifyPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyPrivateKeyIfConflictErrorOccursAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            PrivateKeyDependencyValidationException expectedException =
                CreateAlreadyExistsPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> modifyPrivateKeyTask =
                this.privateKeyService.ModifyPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(modifyPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyPrivateKeyIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> modifyPrivateKeyTask =
                this.privateKeyService.ModifyPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(modifyPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnModifyPrivateKeyIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> modifyPrivateKeyTask =
                this.privateKeyService.ModifyPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(modifyPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyPrivateKeyIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var httpRequestException = new HttpRequestException("Network failure.");

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PrivateKey> modifyPrivateKeyTask =
                this.privateKeyService.ModifyPrivateKeyAsync(somePrivateKey);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(modifyPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyPrivateKeyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            PrivateKey somePrivateKey = CreateRandomPrivateKey();
            var exception = new Exception("Unexpected error.");

            PrivateKeyServiceException expectedException =
                CreateFailedPrivateKeyServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<PrivateKey> modifyPrivateKeyTask =
                this.privateKeyService.ModifyPrivateKeyAsync(somePrivateKey);

            PrivateKeyServiceException actualException =
                await Assert.ThrowsAsync<PrivateKeyServiceException>(modifyPrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
