// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Services.Foundations.Projects;
using Coolify.Resource.Manager.Services.Processings.Projects;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Projects
{
    public partial class ProjectProcessingServiceTests
    {
        private readonly Mock<IProjectService> projectServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IProjectProcessingService projectProcessingService;

        public ProjectProcessingServiceTests()
        {
            this.projectServiceMock = new Mock<IProjectService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.projectProcessingService = new ProjectProcessingService(
                projectService: this.projectServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Project CreateRandomProject() =>
            new Project { Uuid = GetRandomString(), Name = GetRandomString() };

        private static CoolifyEnvironment CreateRandomEnvironment() =>
            new CoolifyEnvironment { Uuid = GetRandomString(), Name = GetRandomString() };
    }
}
