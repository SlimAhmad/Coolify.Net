// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Deployments;
using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Services.Foundations.Deployments;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Deployments
{
    public partial class DeploymentServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IDeploymentService deploymentService;

        public DeploymentServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.deploymentService = new DeploymentService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalDeployment CreateRandomExternalDeployment() =>
            new ExternalDeployment
            {
                Uuid = GetRandomString(),
                ApplicationUuid = GetRandomString(),
                ServerUuid = GetRandomString(),
                Status = GetRandomString(),
                Logs = GetRandomString(),
                CommitSha = GetRandomString(),
                Branch = GetRandomString(),
                ForceRebuild = true,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static Deployment ConvertToDeployment(ExternalDeployment externalDeployment) =>
            new Deployment
            {
                Uuid = externalDeployment.Uuid,
                ApplicationUuid = externalDeployment.ApplicationUuid,
                ServerUuid = externalDeployment.ServerUuid,
                Status = externalDeployment.Status,
                Logs = externalDeployment.Logs,
                CommitSha = externalDeployment.CommitSha,
                Branch = externalDeployment.Branch,
                ForceRebuild = externalDeployment.ForceRebuild,
                CreatedAt = externalDeployment.CreatedAt,
                UpdatedAt = externalDeployment.UpdatedAt
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
