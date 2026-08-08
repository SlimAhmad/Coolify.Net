// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Deployments;
using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Models.Processings.Deployments.Exceptions;
using Coolify.Net.Services.Processings.Deployments;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Deployments
{
    public partial class DeploymentClientTests
    {
        private readonly Mock<IDeploymentProcessingService> deploymentServiceMock;
        private readonly IDeploymentClient deploymentClient;

        public DeploymentClientTests()
        {
            this.deploymentServiceMock = new Mock<IDeploymentProcessingService>();

            this.deploymentClient = new DeploymentClient(
                deploymentProcessingService: this.deploymentServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Deployment CreateRandomDeployment() =>
            new Deployment { Uuid = GetRandomString(), ApplicationUuid = GetRandomString() };

        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DeploymentProcessingValidationException("test", inner),
                new DeploymentProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DeploymentProcessingDependencyException("test", inner),
                new DeploymentProcessingServiceException("test", inner)
            };
        }
    }
}
