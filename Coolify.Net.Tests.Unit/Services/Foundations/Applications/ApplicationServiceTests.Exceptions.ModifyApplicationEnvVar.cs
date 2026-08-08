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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyApplicationEnvVarIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ApplicationDependencyValidationException expectedException =
                CreateInvalidApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyApplicationEnvVarTask =
                this.applicationService.ModifyApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(modifyApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyApplicationEnvVarIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ApplicationDependencyValidationException expectedException =
                CreateAlreadyExistsApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyApplicationEnvVarTask =
                this.applicationService.ModifyApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(modifyApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyApplicationEnvVarIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyApplicationEnvVarTask =
                this.applicationService.ModifyApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(modifyApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnModifyApplicationEnvVarIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyApplicationEnvVarTask =
                this.applicationService.ModifyApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(modifyApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyApplicationEnvVarIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            var httpRequestException = new HttpRequestException("Network failure.");

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyApplicationEnvVarTask =
                this.applicationService.ModifyApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(modifyApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyApplicationEnvVarIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<EnvironmentVariable> modifyApplicationEnvVarTask =
                this.applicationService.ModifyApplicationEnvVarAsync(someApplicationUuid, someEnvironmentVariable);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(modifyApplicationEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(someApplicationUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
