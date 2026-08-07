// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Applications;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Services.Foundations.Applications;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Applications
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
