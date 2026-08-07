// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;
using Coolify.Resource.Manager.Models.Processings.Deployments.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Deployments
{
    public partial class DeploymentProcessingServiceTests
    {
        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> FoundationValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DeploymentValidationException("test", inner),
                new DeploymentDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DeploymentDependencyException("test", inner),
                new DeploymentServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentProcessingService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentProcessingDependencyValidationException>(retrieveAllDeploymentsTask.AsTask);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentProcessingService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentProcessingDependencyException>(retrieveAllDeploymentsTask.AsTask);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Deployment>> retrieveAllDeploymentsTask =
                this.deploymentProcessingService.RetrieveAllDeploymentsAsync();

            await Assert.ThrowsAsync<DeploymentProcessingServiceException>(retrieveAllDeploymentsTask.AsTask);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
