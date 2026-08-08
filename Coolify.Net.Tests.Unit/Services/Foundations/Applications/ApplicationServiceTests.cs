// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Applications;
using Coolify.Net.Models.Externals.EnvironmentVariables;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Services.Foundations.Applications;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IApplicationService applicationService;

        public ApplicationServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.applicationService = new ApplicationService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalApplication CreateRandomExternalApplication() =>
            new ExternalApplication
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                Fqdn = GetRandomString(),
                GitRepository = GetRandomString(),
                GitBranch = GetRandomString(),
                GitCommitSha = GetRandomString(),
                BuildPack = GetRandomString(),
                DockerfileLocation = GetRandomString(),
                DockerComposeLocation = GetRandomString(),
                DockerComposeRaw = GetRandomString(),
                DockerImage = GetRandomString(),
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                EnvironmentUuid = GetRandomString(),
                EnvironmentName = GetRandomString(),
                Status = GetRandomString(),
                InstantDeploy = true,
                AutoDeployEnabled = true,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static Application CreateRandomApplication() =>
            ConvertToApplication(CreateRandomExternalApplication());

        private static Application ConvertToApplication(ExternalApplication externalApplication) =>
            new Application
            {
                Uuid = externalApplication.Uuid,
                Name = externalApplication.Name,
                Description = externalApplication.Description,
                Fqdn = externalApplication.Fqdn,
                GitRepository = externalApplication.GitRepository,
                GitBranch = externalApplication.GitBranch,
                GitCommitSha = externalApplication.GitCommitSha,
                BuildPack = externalApplication.BuildPack,
                DockerfileLocation = externalApplication.DockerfileLocation,
                DockerComposeLocation = externalApplication.DockerComposeLocation,
                DockerComposeRaw = externalApplication.DockerComposeRaw,
                DockerImage = externalApplication.DockerImage,
                ServerUuid = externalApplication.ServerUuid,
                ProjectUuid = externalApplication.ProjectUuid,
                EnvironmentUuid = externalApplication.EnvironmentUuid,
                EnvironmentName = externalApplication.EnvironmentName,
                Status = externalApplication.Status,
                InstantDeploy = externalApplication.InstantDeploy,
                AutoDeployEnabled = externalApplication.AutoDeployEnabled,
                CreatedAt = externalApplication.CreatedAt,
                UpdatedAt = externalApplication.UpdatedAt
            };

        private static ExternalApplication ConvertToExternalApplication(Application application) =>
            new ExternalApplication
            {
                Uuid = application.Uuid,
                Name = application.Name,
                Description = application.Description,
                Fqdn = application.Fqdn,
                GitRepository = application.GitRepository,
                GitBranch = application.GitBranch,
                GitCommitSha = application.GitCommitSha,
                BuildPack = application.BuildPack,
                DockerfileLocation = application.DockerfileLocation,
                DockerComposeLocation = application.DockerComposeLocation,
                DockerComposeRaw = application.DockerComposeRaw,
                DockerImage = application.DockerImage,
                ServerUuid = application.ServerUuid,
                ProjectUuid = application.ProjectUuid,
                EnvironmentUuid = application.EnvironmentUuid,
                EnvironmentName = application.EnvironmentName,
                InstantDeploy = application.InstantDeploy,
                AutoDeployEnabled = application.AutoDeployEnabled
            };

        private static bool IsSameExternalApplication(ExternalApplication actual, ExternalApplication expected) =>
            actual.Uuid == expected.Uuid
            && actual.Name == expected.Name
            && actual.ServerUuid == expected.ServerUuid
            && actual.ProjectUuid == expected.ProjectUuid;

        private static ExternalEnvironmentVariable CreateRandomExternalEnvironmentVariable() =>
            new ExternalEnvironmentVariable
            {
                Uuid = GetRandomString(),
                Key = GetRandomString(),
                Value = GetRandomString(),
                IsPreview = true,
                IsBuildTime = true,
                IsLiteral = true,
                IsMultiline = false,
                IsShownOnce = false,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static EnvironmentVariable CreateRandomEnvironmentVariable() =>
            ConvertToEnvironmentVariable(CreateRandomExternalEnvironmentVariable());

        private static EnvironmentVariable ConvertToEnvironmentVariable(
            ExternalEnvironmentVariable externalEnvironmentVariable) =>
                new EnvironmentVariable
                {
                    Uuid = externalEnvironmentVariable.Uuid,
                    Key = externalEnvironmentVariable.Key,
                    Value = externalEnvironmentVariable.Value,
                    IsPreview = externalEnvironmentVariable.IsPreview,
                    IsBuildTime = externalEnvironmentVariable.IsBuildTime,
                    IsLiteral = externalEnvironmentVariable.IsLiteral,
                    IsMultiline = externalEnvironmentVariable.IsMultiline,
                    IsShownOnce = externalEnvironmentVariable.IsShownOnce,
                    CreatedAt = externalEnvironmentVariable.CreatedAt,
                    UpdatedAt = externalEnvironmentVariable.UpdatedAt
                };

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static ApplicationDependencyValidationException CreateInvalidApplicationDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidApplicationException = new InvalidApplicationException(
                message: "Invalid application.",
                innerException: httpRequestException);

            return new ApplicationDependencyValidationException(
                message: "Application dependency validation error occurred, fix the errors and try again.",
                innerException: invalidApplicationException);
        }

        private static ApplicationDependencyValidationException CreateAlreadyExistsApplicationDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsApplicationException = new AlreadyExistsApplicationException(
                message: "Application already exists.",
                innerException: httpRequestException);

            return new ApplicationDependencyValidationException(
                message: "Application dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsApplicationException);
        }

        private static ApplicationDependencyException CreateFailedApplicationDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedApplicationDependencyException = new FailedApplicationDependencyException(
                message: "Failed application dependency error occurred.",
                innerException: httpRequestException);

            return new ApplicationDependencyException(
                message: "Application dependency error occurred, contact support.",
                innerException: failedApplicationDependencyException);
        }

        private static ApplicationServiceException CreateFailedApplicationServiceException(Exception exception)
        {
            var failedApplicationServiceException = new FailedApplicationServiceException(
                message: "Failed application service error occurred.",
                innerException: exception);

            return new ApplicationServiceException(
                message: "Application service error occurred, contact support.",
                innerException: failedApplicationServiceException);
        }
    }
}
