// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Clients.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Models.Processings.Projects.Exceptions;
using Coolify.Resource.Manager.Services.Processings.Projects;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Projects
{
    public partial class ProjectClientTests
    {
        private readonly Mock<IProjectProcessingService> projectServiceMock;
        private readonly IProjectClient projectClient;

        public ProjectClientTests()
        {
            this.projectServiceMock = new Mock<IProjectProcessingService>();
            this.projectClient = new ProjectClient(projectProcessingService: this.projectServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Project CreateRandomProject() =>
            new Project { Uuid = GetRandomString(), Name = GetRandomString() };

        private static CoolifyEnvironment CreateRandomEnvironment() =>
            new CoolifyEnvironment { Uuid = GetRandomString(), Name = GetRandomString() };

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
                new ProjectProcessingValidationException("test", inner),
                new ProjectProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ProjectProcessingDependencyException("test", inner),
                new ProjectProcessingServiceException("test", inner)
            };
        }
    }
}
