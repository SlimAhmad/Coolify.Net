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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyBulkApplicationEnvVarsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ApplicationDependencyValidationException expectedException =
                CreateInvalidApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkApplicationEnvVarsTask =
                this.applicationService.ModifyBulkApplicationEnvVarsAsync(someApplicationUuid, someEnvironmentVariables);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(modifyBulkApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyBulkApplicationEnvVarsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ApplicationDependencyValidationException expectedException =
                CreateAlreadyExistsApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkApplicationEnvVarsTask =
                this.applicationService.ModifyBulkApplicationEnvVarsAsync(someApplicationUuid, someEnvironmentVariables);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(modifyBulkApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyBulkApplicationEnvVarsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkApplicationEnvVarsTask =
                this.applicationService.ModifyBulkApplicationEnvVarsAsync(someApplicationUuid, someEnvironmentVariables);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(modifyBulkApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnModifyBulkApplicationEnvVarsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkApplicationEnvVarsTask =
                this.applicationService.ModifyBulkApplicationEnvVarsAsync(someApplicationUuid, someEnvironmentVariables);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(modifyBulkApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyBulkApplicationEnvVarsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            var httpRequestException = new HttpRequestException("Network failure.");

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkApplicationEnvVarsTask =
                this.applicationService.ModifyBulkApplicationEnvVarsAsync(someApplicationUuid, someEnvironmentVariables);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(modifyBulkApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyBulkApplicationEnvVarsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkApplicationEnvVarsTask =
                this.applicationService.ModifyBulkApplicationEnvVarsAsync(someApplicationUuid, someEnvironmentVariables);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(modifyBulkApplicationEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(someApplicationUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
