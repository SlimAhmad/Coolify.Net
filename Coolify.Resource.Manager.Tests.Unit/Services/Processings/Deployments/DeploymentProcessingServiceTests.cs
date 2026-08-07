// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Deployments;
using Coolify.Resource.Manager.Services.Foundations.Deployments;
using Coolify.Resource.Manager.Services.Processings.Deployments;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Deployments
{
    public partial class DeploymentProcessingServiceTests
    {
        private readonly Mock<IDeploymentService> deploymentServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IDeploymentProcessingService deploymentProcessingService;

        public DeploymentProcessingServiceTests()
        {
            this.deploymentServiceMock = new Mock<IDeploymentService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.deploymentProcessingService = new DeploymentProcessingService(
                deploymentService: this.deploymentServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Deployment CreateRandomDeployment() =>
            new Deployment { Uuid = GetRandomString(), ApplicationUuid = GetRandomString() };
    }
}
