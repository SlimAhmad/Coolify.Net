// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllApplicationEnvVarsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ApplicationDependencyValidationException expectedException =
                CreateInvalidApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllApplicationEnvVarsTask =
                this.applicationService.RetrieveAllApplicationEnvVarsAsync(someApplicationUuid);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(retrieveAllApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllApplicationEnvVarsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ApplicationDependencyValidationException expectedException =
                CreateAlreadyExistsApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllApplicationEnvVarsTask =
                this.applicationService.RetrieveAllApplicationEnvVarsAsync(someApplicationUuid);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(retrieveAllApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllApplicationEnvVarsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllApplicationEnvVarsTask =
                this.applicationService.RetrieveAllApplicationEnvVarsAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllApplicationEnvVarsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllApplicationEnvVarsTask =
                this.applicationService.RetrieveAllApplicationEnvVarsAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllApplicationEnvVarsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllApplicationEnvVarsTask =
                this.applicationService.RetrieveAllApplicationEnvVarsAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllApplicationEnvVarsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(someApplicationUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllApplicationEnvVarsTask =
                this.applicationService.RetrieveAllApplicationEnvVarsAsync(someApplicationUuid);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(retrieveAllApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
