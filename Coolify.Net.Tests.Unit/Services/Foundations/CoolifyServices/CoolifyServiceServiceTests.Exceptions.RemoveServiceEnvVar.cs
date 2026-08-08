// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
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
        public async Task ShouldThrowDependencyExceptionOnRemoveServiceEnvVarIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            string someEnvironmentVariableUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServiceEnvVarAsync(someServiceUuid, someEnvironmentVariableUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask removeServiceEnvVarTask =
                this.coolifyServiceService.RemoveServiceEnvVarAsync(someServiceUuid, someEnvironmentVariableUuid);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(removeServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServiceEnvVarAsync(someServiceUuid, someEnvironmentVariableUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveServiceEnvVarIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            string someEnvironmentVariableUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServiceEnvVarAsync(someServiceUuid, someEnvironmentVariableUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask removeServiceEnvVarTask =
                this.coolifyServiceService.RemoveServiceEnvVarAsync(someServiceUuid, someEnvironmentVariableUuid);

            CoolifyServiceServiceException actualException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(removeServiceEnvVarTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServiceEnvVarAsync(someServiceUuid, someEnvironmentVariableUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
