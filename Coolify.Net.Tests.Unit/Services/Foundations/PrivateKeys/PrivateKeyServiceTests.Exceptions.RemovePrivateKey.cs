// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.PrivateKeys.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemovePrivateKeyIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            PrivateKeyDependencyException expectedException =
                CreateFailedPrivateKeyDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeletePrivateKeyAsync(somePrivateKeyUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask removePrivateKeyTask =
                this.privateKeyService.RemovePrivateKeyAsync(somePrivateKeyUuid);

            PrivateKeyDependencyException actualException =
                await Assert.ThrowsAsync<PrivateKeyDependencyException>(removePrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeletePrivateKeyAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemovePrivateKeyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string somePrivateKeyUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            PrivateKeyServiceException expectedException =
                CreateFailedPrivateKeyServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeletePrivateKeyAsync(somePrivateKeyUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask removePrivateKeyTask =
                this.privateKeyService.RemovePrivateKeyAsync(somePrivateKeyUuid);

            PrivateKeyServiceException actualException =
                await Assert.ThrowsAsync<PrivateKeyServiceException>(removePrivateKeyTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeletePrivateKeyAsync(somePrivateKeyUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
