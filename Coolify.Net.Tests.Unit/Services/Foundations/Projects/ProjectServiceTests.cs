// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Projects;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Foundations.Projects.Exceptions;
using Coolify.Net.Services.Foundations.Projects;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Projects
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

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static ProjectDependencyValidationException CreateInvalidProjectDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidProjectException = new InvalidProjectException(
                message: "Invalid project.",
                innerException: httpRequestException);

            return new ProjectDependencyValidationException(
                message: "Project dependency validation error occurred, fix the errors and try again.",
                innerException: invalidProjectException);
        }

        private static ProjectDependencyValidationException CreateAlreadyExistsProjectDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsProjectException = new AlreadyExistsProjectException(
                message: "Project already exists.",
                innerException: httpRequestException);

            return new ProjectDependencyValidationException(
                message: "Project dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsProjectException);
        }

        private static ProjectDependencyException CreateFailedProjectDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedProjectDependencyException = new FailedProjectDependencyException(
                message: "Failed project dependency error occurred.",
                innerException: httpRequestException);

            return new ProjectDependencyException(
                message: "Project dependency error occurred, contact support.",
                innerException: failedProjectDependencyException);
        }

        private static ProjectServiceException CreateFailedProjectServiceException(Exception exception)
        {
            var failedProjectServiceException = new FailedProjectServiceException(
                message: "Failed project service error occurred.",
                innerException: exception);

            return new ProjectServiceException(
                message: "Project service error occurred, contact support.",
                innerException: failedProjectServiceException);
        }
    }
}
