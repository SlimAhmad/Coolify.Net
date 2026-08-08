// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllServiceEnvVarsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            CoolifyServiceDependencyValidationException expectedException =
                CreateInvalidCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(someServiceUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllServiceEnvVarsTask =
                this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(someServiceUuid);

            CoolifyServiceDependencyValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveAllServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServiceEnvVarsAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllServiceEnvVarsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            CoolifyServiceDependencyValidationException expectedException =
                CreateAlreadyExistsCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(someServiceUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllServiceEnvVarsTask =
                this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(someServiceUuid);

            CoolifyServiceDependencyValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveAllServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServiceEnvVarsAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllServiceEnvVarsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(someServiceUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllServiceEnvVarsTask =
                this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(someServiceUuid);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServiceEnvVarsAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllServiceEnvVarsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(someServiceUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllServiceEnvVarsTask =
                this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(someServiceUuid);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServiceEnvVarsAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllServiceEnvVarsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(someServiceUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllServiceEnvVarsTask =
                this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(someServiceUuid);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServiceEnvVarsAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllServiceEnvVarsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(someServiceUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<EnvironmentVariable>> retrieveAllServiceEnvVarsTask =
                this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(someServiceUuid);

            CoolifyServiceServiceException actualException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(retrieveAllServiceEnvVarsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetServiceEnvVarsAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
