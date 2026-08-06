// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Clients.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions;
using Coolify.Resource.Manager.Services.Foundations.Projects;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Projects
{
    public partial class ProjectClientTests
    {
        private readonly Mock<IProjectService> projectServiceMock;
        private readonly IProjectClient projectClient;

        public ProjectClientTests()
        {
            this.projectServiceMock = new Mock<IProjectService>();
            this.projectClient = new ProjectClient(projectService: this.projectServiceMock.Object);
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
                new ProjectValidationException("test", inner),
                new ProjectDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ProjectDependencyException("test", inner),
                new ProjectServiceException("test", inner)
            };
        }
    }
}
