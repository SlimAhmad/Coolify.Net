// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.EnvironmentVariables;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyServiceEnvVarIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            CoolifyServiceDependencyValidationException expectedException =
                CreateInvalidCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyServiceEnvVarTask =
                this.coolifyServiceService.ModifyServiceEnvVarAsync(someServiceUuid, someEnvironmentVariable);

            CoolifyServiceDependencyValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(modifyServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyServiceEnvVarIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            CoolifyServiceDependencyValidationException expectedException =
                CreateAlreadyExistsCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyServiceEnvVarTask =
                this.coolifyServiceService.ModifyServiceEnvVarAsync(someServiceUuid, someEnvironmentVariable);

            CoolifyServiceDependencyValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(modifyServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyServiceEnvVarIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyServiceEnvVarTask =
                this.coolifyServiceService.ModifyServiceEnvVarAsync(someServiceUuid, someEnvironmentVariable);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnModifyServiceEnvVarIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyServiceEnvVarTask =
                this.coolifyServiceService.ModifyServiceEnvVarAsync(someServiceUuid, someEnvironmentVariable);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyServiceEnvVarIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            var httpRequestException = new HttpRequestException("Network failure.");

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<EnvironmentVariable> modifyServiceEnvVarTask =
                this.coolifyServiceService.ModifyServiceEnvVarAsync(someServiceUuid, someEnvironmentVariable);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyServiceEnvVarIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            EnvironmentVariable someEnvironmentVariable = CreateRandomEnvironmentVariable();
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<EnvironmentVariable> modifyServiceEnvVarTask =
                this.coolifyServiceService.ModifyServiceEnvVarAsync(someServiceUuid, someEnvironmentVariable);

            CoolifyServiceServiceException actualException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(modifyServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(someServiceUuid, It.IsAny<ExternalEnvironmentVariable>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
