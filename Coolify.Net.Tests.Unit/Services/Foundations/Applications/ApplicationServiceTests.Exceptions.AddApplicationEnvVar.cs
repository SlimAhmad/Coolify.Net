// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.EnvironmentVariables;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddApplicationEnvVarIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ApplicationDependencyValidationException expectedException =
                CreateInvalidApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> addApplicationEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(addApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddApplicationEnvVarIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ApplicationDependencyValidationException expectedException =
                CreateAlreadyExistsApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> addApplicationEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(addApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddApplicationEnvVarIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> addApplicationEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(addApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddApplicationEnvVarIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> addApplicationEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(addApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddApplicationEnvVarIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            var httpRequestException = new HttpRequestException("Network failure.");

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> addApplicationEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(addApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddApplicationEnvVarIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<EnvironmentVariable> addApplicationEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(addApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
