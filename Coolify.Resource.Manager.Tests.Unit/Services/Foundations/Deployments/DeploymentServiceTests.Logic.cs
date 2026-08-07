// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Deployments;
using Coolify.Resource.Manager.Models.Foundations.Deployments;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllDeploymentsAsync()
        {
            List<ExternalDeployment> randomExternalDeployments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalDeployment()).ToList();

            IEnumerable<Deployment> expectedDeployments = randomExternalDeployments.Select(ConvertToDeployment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalDeployments);

            IEnumerable<Deployment> actualDeployments =
                await this.deploymentService.RetrieveAllDeploymentsAsync();

            actualDeployments.Should().BeEquivalentTo(expectedDeployments);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveDeploymentByUuidAsync()
        {
            ExternalDeployment randomExternalDeployment = CreateRandomExternalDeployment();
            string inputDeploymentUuid = randomExternalDeployment.Uuid;
            Deployment expectedDeployment = ConvertToDeployment(randomExternalDeployment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDeploymentByUuidAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalDeployment);

            Deployment actualDeployment =
                await this.deploymentService.RetrieveDeploymentByUuidAsync(inputDeploymentUuid);

            actualDeployment.Should().BeEquivalentTo(expectedDeployment);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDeploymentByUuidAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeployByUuidAsync()
        {
            string inputUuid = GetRandomString();
            ExternalDeployment returnedExternalDeployment = CreateRandomExternalDeployment();
            Deployment expectedDeployment = ConvertToDeployment(returnedExternalDeployment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeployAsync(inputUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalDeployment);

            Deployment actualDeployment = await this.deploymentService.DeployByUuidAsync(inputUuid);

            actualDeployment.Should().BeEquivalentTo(expectedDeployment);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeployAsync(inputUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCancelDeploymentAsync()
        {
            string inputDeploymentUuid = GetRandomString();
            ExternalDeployment returnedExternalDeployment = CreateRandomExternalDeployment();
            Deployment expectedDeployment = ConvertToDeployment(returnedExternalDeployment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDeploymentCancelAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalDeployment);

            Deployment actualDeployment = await this.deploymentService.CancelDeploymentAsync(inputDeploymentUuid);

            actualDeployment.Should().BeEquivalentTo(expectedDeployment);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDeploymentCancelAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveApplicationDeploymentsAsync()
        {
            string inputApplicationUuid = GetRandomString();

            List<ExternalDeployment> randomExternalDeployments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalDeployment()).ToList();

            IEnumerable<Deployment> expectedDeployments = randomExternalDeployments.Select(ConvertToDeployment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationDeploymentsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalDeployments);

            IEnumerable<Deployment> actualDeployments =
                await this.deploymentService.RetrieveApplicationDeploymentsAsync(inputApplicationUuid);

            actualDeployments.Should().BeEquivalentTo(expectedDeployments);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationDeploymentsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
