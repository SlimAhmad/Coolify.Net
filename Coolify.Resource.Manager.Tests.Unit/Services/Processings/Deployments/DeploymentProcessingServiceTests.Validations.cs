// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Models.Processings.Deployments.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Deployments
{
    public partial class DeploymentProcessingServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenDeploymentUuidIsInvalidAndLogItAsync(
            string invalidDeploymentUuid)
        {
            var invalidDeploymentProcessingException =
                new InvalidDeploymentProcessingException(message: "Deployment uuid is invalid.");

            var expectedDeploymentProcessingValidationException =
                new DeploymentProcessingValidationException(
                    message: "Deployment processing validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentProcessingException);

            ValueTask<Deployment> retrieveByUuidTask =
                this.deploymentProcessingService.RetrieveDeploymentByUuidAsync(invalidDeploymentUuid);

            DeploymentProcessingValidationException actualException =
                await Assert.ThrowsAsync<DeploymentProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnDeployByUuidWhenUuidIsInvalidAndLogItAsync(
            string invalidUuid)
        {
            var invalidDeploymentProcessingException =
                new InvalidDeploymentProcessingException(message: "Deployment uuid is invalid.");

            var expectedDeploymentProcessingValidationException =
                new DeploymentProcessingValidationException(
                    message: "Deployment processing validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentProcessingException);

            ValueTask<Deployment> deployTask =
                this.deploymentProcessingService.DeployByUuidAsync(invalidUuid);

            DeploymentProcessingValidationException actualException =
                await Assert.ThrowsAsync<DeploymentProcessingValidationException>(deployTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveApplicationDeploymentsWhenApplicationUuidIsInvalidAndLogItAsync(
            string invalidApplicationUuid)
        {
            var invalidDeploymentProcessingException =
                new InvalidDeploymentProcessingException(message: "Application uuid is invalid.");

            var expectedDeploymentProcessingValidationException =
                new DeploymentProcessingValidationException(
                    message: "Deployment processing validation error occurred, fix the errors and try again.",
                    innerException: invalidDeploymentProcessingException);

            ValueTask<IEnumerable<Deployment>> retrieveTask =
                this.deploymentProcessingService.RetrieveApplicationDeploymentsAsync(invalidApplicationUuid);

            DeploymentProcessingValidationException actualException =
                await Assert.ThrowsAsync<DeploymentProcessingValidationException>(retrieveTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDeploymentProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
