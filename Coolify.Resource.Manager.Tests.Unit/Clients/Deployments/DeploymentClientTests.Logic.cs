// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Deployments
{
    public partial class DeploymentClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllDeploymentsAsync()
        {
            IEnumerable<Deployment> randomDeployments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomDeployment());

            this.deploymentServiceMock
                .Setup(service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDeployments);

            IEnumerable<Deployment> actualDeployments = await this.deploymentClient.RetrieveAllDeploymentsAsync();

            actualDeployments.Should().BeEquivalentTo(randomDeployments);

            this.deploymentServiceMock.Verify(
                service => service.RetrieveAllDeploymentsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveDeploymentByUuidAsync()
        {
            Deployment randomDeployment = CreateRandomDeployment();
            string inputDeploymentUuid = randomDeployment.Uuid;

            this.deploymentServiceMock
                .Setup(service => service.RetrieveDeploymentByUuidAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDeployment);

            Deployment actualDeployment =
                await this.deploymentClient.RetrieveDeploymentByUuidAsync(inputDeploymentUuid);

            actualDeployment.Should().BeEquivalentTo(randomDeployment);

            this.deploymentServiceMock.Verify(service =>
                service.RetrieveDeploymentByUuidAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDeployByUuidAsync()
        {
            string inputUuid = GetRandomString();
            Deployment randomDeployment = CreateRandomDeployment();

            this.deploymentServiceMock
                .Setup(service => service.DeployByUuidAsync(inputUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDeployment);

            Deployment actualDeployment = await this.deploymentClient.DeployByUuidAsync(inputUuid);

            actualDeployment.Should().BeEquivalentTo(randomDeployment);

            this.deploymentServiceMock.Verify(service =>
                service.DeployByUuidAsync(inputUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCancelDeploymentAsync()
        {
            string inputDeploymentUuid = GetRandomString();
            Deployment randomDeployment = CreateRandomDeployment();

            this.deploymentServiceMock
                .Setup(service => service.CancelDeploymentAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDeployment);

            Deployment actualDeployment = await this.deploymentClient.CancelDeploymentAsync(inputDeploymentUuid);

            actualDeployment.Should().BeEquivalentTo(randomDeployment);

            this.deploymentServiceMock.Verify(service =>
                service.CancelDeploymentAsync(inputDeploymentUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveApplicationDeploymentsAsync()
        {
            string inputApplicationUuid = GetRandomString();

            IEnumerable<Deployment> randomDeployments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomDeployment());

            this.deploymentServiceMock
                .Setup(service => service.RetrieveApplicationDeploymentsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDeployments);

            IEnumerable<Deployment> actualDeployments =
                await this.deploymentClient.RetrieveApplicationDeploymentsAsync(inputApplicationUuid);

            actualDeployments.Should().BeEquivalentTo(randomDeployments);

            this.deploymentServiceMock.Verify(service =>
                service.RetrieveApplicationDeploymentsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.deploymentServiceMock.VerifyNoOtherCalls();
        }
    }
}
