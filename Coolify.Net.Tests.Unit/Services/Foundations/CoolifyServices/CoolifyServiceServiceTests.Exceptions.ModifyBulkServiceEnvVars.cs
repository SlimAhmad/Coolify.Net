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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyBulkServiceEnvVarsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            CoolifyServiceDependencyValidationException expectedException =
                CreateInvalidCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkServiceEnvVarsTask =
                this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(someServiceUuid, someEnvironmentVariables);

            CoolifyServiceDependencyValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(modifyBulkServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyBulkServiceEnvVarsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            CoolifyServiceDependencyValidationException expectedException =
                CreateAlreadyExistsCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkServiceEnvVarsTask =
                this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(someServiceUuid, someEnvironmentVariables);

            CoolifyServiceDependencyValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(modifyBulkServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyBulkServiceEnvVarsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkServiceEnvVarsTask =
                this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(someServiceUuid, someEnvironmentVariables);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyBulkServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnModifyBulkServiceEnvVarsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkServiceEnvVarsTask =
                this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(someServiceUuid, someEnvironmentVariables);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyBulkServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyBulkServiceEnvVarsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            var httpRequestException = new HttpRequestException("Network failure.");

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkServiceEnvVarsTask =
                this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(someServiceUuid, someEnvironmentVariables);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyBulkServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyBulkServiceEnvVarsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            List<EnvironmentVariable> someEnvironmentVariables = new List<EnvironmentVariable> { CreateRandomEnvironmentVariable() };
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> modifyBulkServiceEnvVarsTask =
                this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(someServiceUuid, someEnvironmentVariables);

            CoolifyServiceServiceException actualException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(modifyBulkServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(someServiceUuid, It.IsAny<IEnumerable<ExternalEnvironmentVariable>>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
