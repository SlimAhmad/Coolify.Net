// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddPublicWhenApplicationIsNullAndLogItAsync()
        {
            Application nullApplication = null;

            var nullApplicationException = new NullApplicationException(message: "Application is null.");

            var expectedApplicationValidationException =
                new ApplicationValidationException(
                    message: "Application validation error occurred, fix the errors and try again.",
                    innerException: nullApplicationException);

            ValueTask<Application> addApplicationTask =
                this.applicationService.AddPublicApplicationAsync(nullApplication);

            ApplicationValidationException actualException =
                await Assert.ThrowsAsync<ApplicationValidationException>(addApplicationTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddPublicWhenApplicationIsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidApplication = new Application
            {
                Name = invalidText,
                ServerUuid = invalidText,
                ProjectUuid = invalidText
            };

            var invalidApplicationException =
                new InvalidApplicationException(
                    message: "Invalid application. Please fix the errors and try again.");

            invalidApplicationException.UpsertDataList(key: nameof(Application.Name), value: "Text is required");
            invalidApplicationException.UpsertDataList(key: nameof(Application.ServerUuid), value: "Text is required");
            invalidApplicationException.UpsertDataList(key: nameof(Application.ProjectUuid), value: "Text is required");

            var expectedApplicationValidationException =
                new ApplicationValidationException(
                    message: "Application validation error occurred, fix the errors and try again.",
                    innerException: invalidApplicationException);

            ValueTask<Application> addApplicationTask =
                this.applicationService.AddPublicApplicationAsync(invalidApplication);

            ApplicationValidationException actualException =
                await Assert.ThrowsAsync<ApplicationValidationException>(addApplicationTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenApplicationUuidIsInvalidAndLogItAsync(
            string invalidApplicationUuid)
        {
            var invalidApplicationException =
                new InvalidApplicationException(
                    message: "Invalid application. Please fix the errors and try again.");

            invalidApplicationException.UpsertDataList(key: "applicationUuid", value: "Text is required");

            var expectedApplicationValidationException =
                new ApplicationValidationException(
                    message: "Application validation error occurred, fix the errors and try again.",
                    innerException: invalidApplicationException);

            ValueTask<Application> retrieveByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(invalidApplicationUuid);

            ApplicationValidationException actualException =
                await Assert.ThrowsAsync<ApplicationValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenApplicationUuidIsInvalidAndLogItAsync(
            string invalidApplicationUuid)
        {
            var invalidApplicationException =
                new InvalidApplicationException(
                    message: "Invalid application. Please fix the errors and try again.");

            invalidApplicationException.UpsertDataList(key: "applicationUuid", value: "Text is required");

            var expectedApplicationValidationException =
                new ApplicationValidationException(
                    message: "Application validation error occurred, fix the errors and try again.",
                    innerException: invalidApplicationException);

            ValueTask removeApplicationTask =
                this.applicationService.RemoveApplicationAsync(invalidApplicationUuid);

            ApplicationValidationException actualException =
                await Assert.ThrowsAsync<ApplicationValidationException>(removeApplicationTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddEnvVarWhenEnvironmentVariableIsNullAndLogItAsync()
        {
            string someApplicationUuid = GetRandomString();
            EnvironmentVariable nullEnvironmentVariable = null;

            var nullApplicationException = new NullApplicationException(message: "Environment variable is null.");

            var expectedApplicationValidationException =
                new ApplicationValidationException(
                    message: "Application validation error occurred, fix the errors and try again.",
                    innerException: nullApplicationException);

            ValueTask<EnvironmentVariable> addEnvVarTask =
                this.applicationService.AddApplicationEnvVarAsync(someApplicationUuid, nullEnvironmentVariable);

            ApplicationValidationException actualException =
                await Assert.ThrowsAsync<ApplicationValidationException>(addEnvVarTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnStartWhenApplicationUuidIsInvalidAndLogItAsync(
            string invalidApplicationUuid)
        {
            var invalidApplicationException =
                new InvalidApplicationException(
                    message: "Invalid application. Please fix the errors and try again.");

            invalidApplicationException.UpsertDataList(key: "applicationUuid", value: "Text is required");

            var expectedApplicationValidationException =
                new ApplicationValidationException(
                    message: "Application validation error occurred, fix the errors and try again.",
                    innerException: invalidApplicationException);

            ValueTask startApplicationTask =
                this.applicationService.StartApplicationAsync(invalidApplicationUuid);

            ApplicationValidationException actualException =
                await Assert.ThrowsAsync<ApplicationValidationException>(startApplicationTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedApplicationValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
