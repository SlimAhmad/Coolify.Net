// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Deployments.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Deployments;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Deployments
{
    public partial class DeploymentClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new DeploymentClientValidationException(
                message: "Deployment client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<Deployment>> task = this.deploymentClient.RetrieveAllDeploymentsAsync();

            DeploymentClientValidationException actual =
                await Assert.ThrowsAsync<DeploymentClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new DeploymentClientDependencyException(
                message: "Deployment client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<Deployment>> task = this.deploymentClient.RetrieveAllDeploymentsAsync();

            DeploymentClientDependencyException actual =
                await Assert.ThrowsAsync<DeploymentClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Deployment>> task = this.deploymentClient.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentClientServiceException>(task.AsTask);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<Deployment>> task = this.deploymentClient.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnDeployByUuidWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someUuid = GetRandomString();

            var expected = new DeploymentClientValidationException(
                message: "Deployment client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.deploymentServiceMock
                .Setup(service => service.DeployByUuidAsync(someUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Deployment> task = this.deploymentClient.DeployByUuidAsync(someUuid);

            DeploymentClientValidationException actual =
                await Assert.ThrowsAsync<DeploymentClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.deploymentServiceMock.Verify(service =>
                service.DeployByUuidAsync(someUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnDeployByUuidWhenExceptionOccursAsync()
        {
            string someUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.deploymentServiceMock
                .Setup(service => service.DeployByUuidAsync(someUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Deployment> task = this.deploymentClient.DeployByUuidAsync(someUuid);

            await Assert.ThrowsAsync<DeploymentClientServiceException>(task.AsTask);

            this.deploymentServiceMock.Verify(service =>
                service.DeployByUuidAsync(someUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnCancelWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someDeploymentUuid = GetRandomString();

            var expected = new DeploymentClientValidationException(
                message: "Deployment client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.deploymentServiceMock
                .Setup(service => service.CancelDeploymentAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Deployment> task = this.deploymentClient.CancelDeploymentAsync(someDeploymentUuid);

            DeploymentClientValidationException actual =
                await Assert.ThrowsAsync<DeploymentClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.deploymentServiceMock.Verify(service =>
                service.CancelDeploymentAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnCancelWhenExceptionOccursAsync()
        {
            string someDeploymentUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.deploymentServiceMock
                .Setup(service => service.CancelDeploymentAsync(someDeploymentUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Deployment> task = this.deploymentClient.CancelDeploymentAsync(someDeploymentUuid);

            await Assert.ThrowsAsync<DeploymentClientServiceException>(task.AsTask);

            this.deploymentServiceMock.Verify(service =>
                service.CancelDeploymentAsync(someDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }
    }
}
