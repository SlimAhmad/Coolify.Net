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
        public async Task ShouldThrowDependencyExceptionOnRemoveApplicationEnvVarIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            string someEnvironmentVariableUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariableUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask removeApplicationEnvVarTask =
                this.applicationService.RemoveApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariableUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(removeApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariableUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveApplicationEnvVarIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            string someEnvironmentVariableUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariableUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask removeApplicationEnvVarTask =
                this.applicationService.RemoveApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariableUuid);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(removeApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariableUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
