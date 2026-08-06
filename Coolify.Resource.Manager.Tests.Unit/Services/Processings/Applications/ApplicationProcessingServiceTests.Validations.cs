// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Processings.Applications.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Applications
{
    public partial class ApplicationProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddPublicWhenApplicationIsNullAndLogItAsync()
        {
            Application nullApplication = null;

            var nullApplicationProcessingException =
                new NullApplicationProcessingException(message: "Application is null.");

            var expectedApplicationProcessingValidationException =
                new ApplicationProcessingValidationException(
                    message: "Application processing validation error occurred, fix the errors and try again.",
                    innerException: nullApplicationProcessingException);

            ValueTask<Application> addApplicationTask =
                this.applicationProcessingService.AddPublicApplicationAsync(nullApplication);

            ApplicationProcessingValidationException actualException =
                await Assert.ThrowsAsync<ApplicationProcessingValidationException>(addApplicationTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenApplicationUuidIsInvalidAndLogItAsync(
            string invalidApplicationUuid)
        {
            var invalidApplicationProcessingException =
                new InvalidApplicationProcessingException(message: "Application uuid is invalid.");

            var expectedApplicationProcessingValidationException =
                new ApplicationProcessingValidationException(
                    message: "Application processing validation error occurred, fix the errors and try again.",
                    innerException: invalidApplicationProcessingException);

            ValueTask<Application> retrieveByUuidTask =
                this.applicationProcessingService.RetrieveApplicationByUuidAsync(invalidApplicationUuid);

            ApplicationProcessingValidationException actualException =
                await Assert.ThrowsAsync<ApplicationProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddEnvVarWhenEnvironmentVariableIsNullAndLogItAsync()
        {
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable nullEnvironmentVariable = null;

            var nullApplicationProcessingException =
                new NullApplicationProcessingException(message: "Environment variable is null.");

            var expectedApplicationProcessingValidationException =
                new ApplicationProcessingValidationException(
                    message: "Application processing validation error occurred, fix the errors and try again.",
                    innerException: nullApplicationProcessingException);

            ValueTask<EnvironmentVariable> addEnvVarTask =
                this.applicationProcessingService.AddApplicationEnvVarAsync(someApplicationUuid, nullEnvironmentVariable);

            ApplicationProcessingValidationException actualException =
                await Assert.ThrowsAsync<ApplicationProcessingValidationException>(addEnvVarTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
