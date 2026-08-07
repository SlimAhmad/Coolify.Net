// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenDeploymentUuidIsInvalidAndLogItAsync(
            string invalidDeploymentUuid)
        {
            var invalidDeploymentException =
                new InvalidDeploymentException(
                    message: "Invalid deployment. Please fix the errors and try again.");

            invalidDeploymentException.UpsertDataList(key: "deploymentUuid", value: "Text is required");

            var expectedDeploymentValidationException =
                new DeploymentValidationException(
                    message: "Deployment validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentException);

            ValueTask<Deployment> retrieveByUuidTask =
                this.deploymentService.RetrieveDeploymentByUuidAsync(invalidDeploymentUuid);

            DeploymentValidationException actualException =
                await Assert.ThrowsAsync<DeploymentValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnDeployByUuidWhenUuidIsInvalidAndLogItAsync(
            string invalidUuid)
        {
            var invalidDeploymentException =
                new InvalidDeploymentException(
                    message: "Invalid deployment. Please fix the errors and try again.");

            invalidDeploymentException.UpsertDataList(key: "deploymentUuid", value: "Text is required");

            var expectedDeploymentValidationException =
                new DeploymentValidationException(
                    message: "Deployment validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentException);

            ValueTask<Deployment> deployTask =
                this.deploymentService.DeployByUuidAsync(invalidUuid);

            DeploymentValidationException actualException =
                await Assert.ThrowsAsync<DeploymentValidationException>(deployTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnCancelWhenDeploymentUuidIsInvalidAndLogItAsync(
            string invalidDeploymentUuid)
        {
            var invalidDeploymentException =
                new InvalidDeploymentException(
                    message: "Invalid deployment. Please fix the errors and try again.");

            invalidDeploymentException.UpsertDataList(key: "deploymentUuid", value: "Text is required");

            var expectedDeploymentValidationException =
                new DeploymentValidationException(
                    message: "Deployment validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentException);

            ValueTask<Deployment> cancelTask =
                this.deploymentService.CancelDeploymentAsync(invalidDeploymentUuid);

            DeploymentValidationException actualException =
                await Assert.ThrowsAsync<DeploymentValidationException>(cancelTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveApplicationDeploymentsWhenApplicationUuidIsInvalidAndLogItAsync(
            string invalidApplicationUuid)
        {
            var invalidDeploymentException =
                new InvalidDeploymentException(
                    message: "Invalid deployment. Please fix the errors and try again.");

            invalidDeploymentException.UpsertDataList(key: "applicationUuid", value: "Text is required");

            var expectedDeploymentValidationException =
                new DeploymentValidationException(
                    message: "Deployment validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentException);

            ValueTask<IEnumerable<Deployment>> retrieveTask =
                this.deploymentService.RetrieveApplicationDeploymentsAsync(invalidApplicationUuid);

            DeploymentValidationException actualException =
                await Assert.ThrowsAsync<DeploymentValidationException>(retrieveTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
