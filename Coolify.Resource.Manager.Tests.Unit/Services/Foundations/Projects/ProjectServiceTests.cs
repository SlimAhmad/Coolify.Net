// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Services.Foundations.Projects;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IProjectService projectService;

        public ProjectServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.projectService = new ProjectService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalProject CreateRandomExternalProject() =>
            new ExternalProject
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static Project CreateRandomProject() =>
            ConvertToProject(CreateRandomExternalProject());

        private static Project ConvertToProject(ExternalProject externalProject) =>
            new Project
            {
                Uuid = externalProject.Uuid,
                Name = externalProject.Name,
                Description = externalProject.Description,
                CreatedAt = externalProject.CreatedAt,
                UpdatedAt = externalProject.UpdatedAt
            };

        private static ExternalProject ConvertToExternalProject(Project project) =>
            new ExternalProject
            {
                Uuid = project.Uuid,
                Name = project.Name,
                Description = project.Description
            };

        private static ExternalCoolifyEnvironment CreateRandomExternalEnvironment() =>
            new ExternalCoolifyEnvironment
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                ProjectUuid = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static CoolifyEnvironment CreateRandomEnvironment() =>
            ConvertToEnvironment(CreateRandomExternalEnvironment());

        private static CoolifyEnvironment ConvertToEnvironment(ExternalCoolifyEnvironment externalEnvironment) =>
            new CoolifyEnvironment
            {
                Uuid = externalEnvironment.Uuid,
                Name = externalEnvironment.Name,
                Description = externalEnvironment.Description,
                ProjectUuid = externalEnvironment.ProjectUuid,
                CreatedAt = externalEnvironment.CreatedAt,
                UpdatedAt = externalEnvironment.UpdatedAt
            };

        private static ExternalCoolifyEnvironment ConvertToExternalEnvironment(CoolifyEnvironment environment) =>
            new ExternalCoolifyEnvironment
            {
                Uuid = environment.Uuid,
                Name = environment.Name,
                Description = environment.Description,
                ProjectUuid = environment.ProjectUuid
            };

        public static TheoryData<HttpStatusCode> DependencyValidationHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.BadRequest,
                HttpStatusCode.Conflict
            };

        public static TheoryData<HttpStatusCode> CriticalDependencyHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
                HttpStatusCode.NotFound
            };

        public static TheoryData<HttpStatusCode> DependencyHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.TooManyRequests,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.InternalServerError
            };

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);
    }
}
