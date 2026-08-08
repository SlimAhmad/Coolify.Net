// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
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
        public async Task ShouldThrowDependencyExceptionOnRestartApplicationIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationRestartAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask restartApplicationTask =
                this.applicationService.RestartApplicationAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(restartApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationRestartAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRestartApplicationIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationRestartAsync(someApplicationUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask restartApplicationTask =
                this.applicationService.RestartApplicationAsync(someApplicationUuid);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(restartApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationRestartAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
