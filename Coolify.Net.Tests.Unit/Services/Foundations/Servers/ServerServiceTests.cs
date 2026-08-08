// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Servers;
using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Coolify.Net.Services.Foundations.Servers;
using Moq;
using Xeptions;
using Xunit;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
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

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static ServerDependencyValidationException CreateInvalidServerDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidServerException = new InvalidServerException(
                message: "Invalid server.",
                innerException: httpRequestException);

            return new ServerDependencyValidationException(
                message: "Server dependency validation error occurred, fix the errors and try again.",
                innerException: invalidServerException);
        }

        private static ServerDependencyValidationException CreateAlreadyExistsServerDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsServerException = new AlreadyExistsServerException(
                message: "Server already exists.",
                innerException: httpRequestException);

            return new ServerDependencyValidationException(
                message: "Server dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsServerException);
        }

        private static ServerDependencyException CreateFailedServerDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedServerDependencyException = new FailedServerDependencyException(
                message: "Failed server dependency error occurred.",
                innerException: httpRequestException);

            return new ServerDependencyException(
                message: "Server dependency error occurred, contact support.",
                innerException: failedServerDependencyException);
        }

        private static ServerServiceException CreateFailedServerServiceException(Exception exception)
        {
            var failedServerServiceException = new FailedServerServiceException(
                message: "Failed server service error occurred.",
                innerException: exception);

            return new ServerServiceException(
                message: "Server service error occurred, contact support.",
                innerException: failedServerServiceException);
        }
    }
}
