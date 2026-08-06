// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Services.Foundations.Servers;
using Moq;
using Xunit;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IServerService serverService;

        public ServerServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.serverService = new ServerService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static int GetRandomNumber() => new Random().Next(1, 9999);

        private static ExternalServer CreateRandomExternalServer() =>
            new ExternalServer
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                Ip = GetRandomString(),
                User = GetRandomString(),
                Port = GetRandomNumber(),
                PrivateKeyUuid = GetRandomString(),
                ProxyEnabled = true,
                ProxyType = GetRandomString(),
                IsReachable = true,
                IsUsable = true,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,

                Settings = new ExternalServerSetting
                {
                    IsBuildServer = true,
                    IsSwarmManager = false,
                    IsSwarmWorker = false,
                    SentinelEnabled = true,
                    SentinelToken = GetRandomString(),
                    IsReachable = true,
                    IsUsable = true,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow
                }
            };

        private static Server CreateRandomServer() =>
            ConvertToServer(CreateRandomExternalServer());

        private static Server ConvertToServer(ExternalServer externalServer) =>
            new Server
            {
                Uuid = externalServer.Uuid,
                Name = externalServer.Name,
                Description = externalServer.Description,
                Ip = externalServer.Ip,
                User = externalServer.User,
                Port = externalServer.Port,
                PrivateKeyUuid = externalServer.PrivateKeyUuid,
                ProxyEnabled = externalServer.ProxyEnabled,
                ProxyType = externalServer.ProxyType,
                IsReachable = externalServer.IsReachable,
                IsUsable = externalServer.IsUsable,
                CreatedAt = externalServer.CreatedAt,
                UpdatedAt = externalServer.UpdatedAt,

                Settings = externalServer.Settings is null ? null : new ServerSetting
                {
                    IsBuildServer = externalServer.Settings.IsBuildServer,
                    IsSwarmManager = externalServer.Settings.IsSwarmManager,
                    IsSwarmWorker = externalServer.Settings.IsSwarmWorker,
                    SentinelEnabled = externalServer.Settings.SentinelEnabled,
                    SentinelToken = externalServer.Settings.SentinelToken,
                    IsReachable = externalServer.Settings.IsReachable,
                    IsUsable = externalServer.Settings.IsUsable,
                    CreatedAt = externalServer.Settings.CreatedAt,
                    UpdatedAt = externalServer.Settings.UpdatedAt
                }
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
