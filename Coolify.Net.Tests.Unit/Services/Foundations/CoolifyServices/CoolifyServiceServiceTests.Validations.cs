// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenServiceIsNullAndLogItAsync()
        {
            CoolifyService nullCoolifyService = null;

            var nullCoolifyServiceException = new NullCoolifyServiceException(message: "Service is null.");

            var expectedCoolifyServiceValidationException =
                new CoolifyServiceValidationException(
                    message: "Service validation error occurred, fix the errors and try again.",
                    innerException: nullCoolifyServiceException);

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceService.AddCoolifyServiceAsync(nullCoolifyService);

            CoolifyServiceValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceValidationException>(addCoolifyServiceTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddWhenServiceIsInvalidAndLogItAsync(
            string invalidText)
        {
            var invalidCoolifyService = new CoolifyService
            {
                Name = invalidText,
                ServerUuid = invalidText,
                ProjectUuid = invalidText
            };

            var invalidCoolifyServiceException =
                new InvalidCoolifyServiceException(
                    message: "Invalid service. Please fix the errors and try again.");

            invalidCoolifyServiceException.UpsertDataList(key: nameof(CoolifyService.Name), value: "Text is required");
            invalidCoolifyServiceException.UpsertDataList(key: nameof(CoolifyService.ServerUuid), value: "Text is required");
            invalidCoolifyServiceException.UpsertDataList(key: nameof(CoolifyService.ProjectUuid), value: "Text is required");

            var expectedCoolifyServiceValidationException =
                new CoolifyServiceValidationException(
                    message: "Service validation error occurred, fix the errors and try again.",
                    innerException: invalidCoolifyServiceException);

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceService.AddCoolifyServiceAsync(invalidCoolifyService);

            CoolifyServiceValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceValidationException>(addCoolifyServiceTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenServiceUuidIsInvalidAndLogItAsync(
            string invalidServiceUuid)
        {
            var invalidCoolifyServiceException =
                new InvalidCoolifyServiceException(
                    message: "Invalid service. Please fix the errors and try again.");

            invalidCoolifyServiceException.UpsertDataList(key: "serviceUuid", value: "Text is required");

            var expectedCoolifyServiceValidationException =
                new CoolifyServiceValidationException(
                    message: "Service validation error occurred, fix the errors and try again.",
                    innerException: invalidCoolifyServiceException);

            ValueTask<CoolifyService> retrieveByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(invalidServiceUuid);

            CoolifyServiceValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenServiceUuidIsInvalidAndLogItAsync(
            string invalidServiceUuid)
        {
            var invalidCoolifyServiceException =
                new InvalidCoolifyServiceException(
                    message: "Invalid service. Please fix the errors and try again.");

            invalidCoolifyServiceException.UpsertDataList(key: "serviceUuid", value: "Text is required");

            var expectedCoolifyServiceValidationException =
                new CoolifyServiceValidationException(
                    message: "Service validation error occurred, fix the errors and try again.",
                    innerException: invalidCoolifyServiceException);

            ValueTask removeCoolifyServiceTask =
                this.coolifyServiceService.RemoveCoolifyServiceAsync(invalidServiceUuid);

            CoolifyServiceValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceValidationException>(removeCoolifyServiceTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddEnvVarWhenEnvironmentVariableIsNullAndLogItAsync()
        {
            string someServiceUuid = GetRandomString();
            EnvironmentVariable nullEnvironmentVariable = null;

            var nullCoolifyServiceException = new NullCoolifyServiceException(message: "Environment variable is null.");

            var expectedCoolifyServiceValidationException =
                new CoolifyServiceValidationException(
                    message: "Service validation error occurred, fix the errors and try again.",
                    innerException: nullCoolifyServiceException);

            ValueTask<EnvironmentVariable> addEnvVarTask =
                this.coolifyServiceService.AddServiceEnvVarAsync(someServiceUuid, nullEnvironmentVariable);

            CoolifyServiceValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceValidationException>(addEnvVarTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnStartWhenServiceUuidIsInvalidAndLogItAsync(
            string invalidServiceUuid)
        {
            var invalidCoolifyServiceException =
                new InvalidCoolifyServiceException(
                    message: "Invalid service. Please fix the errors and try again.");

            invalidCoolifyServiceException.UpsertDataList(key: "serviceUuid", value: "Text is required");

            var expectedCoolifyServiceValidationException =
                new CoolifyServiceValidationException(
                    message: "Service validation error occurred, fix the errors and try again.",
                    innerException: invalidCoolifyServiceException);

            ValueTask startServiceTask =
                this.coolifyServiceService.StartServiceAsync(invalidServiceUuid);

            CoolifyServiceValidationException actualException =
                await Assert.ThrowsAsync<CoolifyServiceValidationException>(startServiceTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedCoolifyServiceValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
