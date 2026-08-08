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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllPrivateKeysIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            PrivateKeyDependencyValidationException expectedException =
                CreateInvalidPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(retrieveAllPrivateKeysTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllPrivateKeysAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllPrivateKeysIfConflictErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            PrivateKeyDependencyValidationException expectedException =
                CreateAlreadyExistsPrivateKeyDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            PrivateKeyDependencyValidationException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyValidationException>(retrieveAllPrivateKeysTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllPrivateKeysAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllPrivateKeysIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllPrivateKeysAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllPrivateKeysIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllPrivateKeysAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllPrivateKeysIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            
            var httpRequestException = new HttpRequestException("Network failure.");

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(retrieveAllPrivateKeysTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllPrivateKeysAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllPrivateKeysIfServiceErrorOccursAndLogItAsync()
        {
            // given
            
            var exception = new Exception("Unexpected error.");

            PrivateKeyServiceException expectedException =
                CreateFailedPrivateKeyServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<PrivateKey>> retrieveAllPrivateKeysTask =
                this.privateKeyService.RetrieveAllPrivateKeysAsync();

            PrivateKeyServiceException actualException =
                await Assert.ThrowsAsync<PrivateKeyServiceException>(retrieveAllPrivateKeysTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllPrivateKeysAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
