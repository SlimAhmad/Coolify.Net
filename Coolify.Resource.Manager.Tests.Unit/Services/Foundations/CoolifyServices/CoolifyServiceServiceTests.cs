// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.CoolifyServices;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Services.Foundations.CoolifyServices;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICoolifyServiceService coolifyServiceService;

        public CoolifyServiceServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.coolifyServiceService = new CoolifyServiceService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalCoolifyService CreateRandomExternalCoolifyService() =>
            new ExternalCoolifyService
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                DockerComposeRaw = GetRandomString(),
                ServiceType = GetRandomString(),
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                EnvironmentUuid = GetRandomString(),
                EnvironmentName = GetRandomString(),
                InstantDeploy = true,
                Status = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static CoolifyService CreateRandomCoolifyService() =>
            ConvertToCoolifyService(CreateRandomExternalCoolifyService());

        private static CoolifyService ConvertToCoolifyService(ExternalCoolifyService externalCoolifyService) =>
            new CoolifyService
            {
                Uuid = externalCoolifyService.Uuid,
                Name = externalCoolifyService.Name,
                Description = externalCoolifyService.Description,
                DockerComposeRaw = externalCoolifyService.DockerComposeRaw,
                ServiceType = externalCoolifyService.ServiceType,
                ServerUuid = externalCoolifyService.ServerUuid,
                ProjectUuid = externalCoolifyService.ProjectUuid,
                EnvironmentUuid = externalCoolifyService.EnvironmentUuid,
                EnvironmentName = externalCoolifyService.EnvironmentName,
                InstantDeploy = externalCoolifyService.InstantDeploy,
                Status = externalCoolifyService.Status,
                CreatedAt = externalCoolifyService.CreatedAt,
                UpdatedAt = externalCoolifyService.UpdatedAt
            };

        private static ExternalCoolifyService ConvertToExternalCoolifyService(CoolifyService service) =>
            new ExternalCoolifyService
            {
                Uuid = service.Uuid,
                Name = service.Name,
                Description = service.Description,
                DockerComposeRaw = service.DockerComposeRaw,
                ServiceType = service.ServiceType,
                ServerUuid = service.ServerUuid,
                ProjectUuid = service.ProjectUuid,
                EnvironmentUuid = service.EnvironmentUuid,
                EnvironmentName = service.EnvironmentName,
                InstantDeploy = service.InstantDeploy
            };

        private static bool IsSameExternalCoolifyService(ExternalCoolifyService actual, ExternalCoolifyService expected) =>
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
