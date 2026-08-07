// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.CoolifyServices
{
    public partial class CoolifyServiceProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenServiceIsNullAndLogItAsync()
        {
            CoolifyService nullCoolifyService = null;

            var nullCoolifyServiceProcessingException =
                new NullCoolifyServiceProcessingException(message: "Service is null.");

            var expectedCoolifyServiceProcessingValidationException =
                new CoolifyServiceProcessingValidationException(
                    message: "Service processing validation error occurred, fix the errors and try again.",
                    innerException: nullCoolifyServiceProcessingException);

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceProcessingService.AddCoolifyServiceAsync(nullCoolifyService);

            CoolifyServiceProcessingValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceProcessingValidationException>(addCoolifyServiceTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenServiceUuidIsInvalidAndLogItAsync(
            string invalidServiceUuid)
        {
            var invalidCoolifyServiceProcessingException =
                new InvalidCoolifyServiceProcessingException(message: "Service uuid is invalid.");

            var expectedCoolifyServiceProcessingValidationException =
                new CoolifyServiceProcessingValidationException(
                    message: "Service processing validation error occurred, fix the errors and try again.",
                    innerException: invalidCoolifyServiceProcessingException);

            ValueTask<CoolifyService> retrieveByUuidTask =
                this.coolifyServiceProcessingService.RetrieveServiceByUuidAsync(invalidServiceUuid);

            CoolifyServiceProcessingValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddEnvVarWhenEnvironmentVariableIsNullAndLogItAsync()
        {
            string someServiceUuid = GetRandomString();
            EnvironmentVariable nullEnvironmentVariable = null;

            var nullCoolifyServiceProcessingException =
                new NullCoolifyServiceProcessingException(message: "Environment variable is null.");

            var expectedCoolifyServiceProcessingValidationException =
                new CoolifyServiceProcessingValidationException(
                    message: "Service processing validation error occurred, fix the errors and try again.",
                    innerException: nullCoolifyServiceProcessingException);

            ValueTask<EnvironmentVariable> addEnvVarTask =
                this.coolifyServiceProcessingService.AddServiceEnvVarAsync(someServiceUuid, nullEnvironmentVariable);

            CoolifyServiceProcessingValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceProcessingValidationException>(addEnvVarTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
