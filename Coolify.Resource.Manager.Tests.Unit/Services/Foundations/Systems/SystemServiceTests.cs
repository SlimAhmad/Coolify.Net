// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Systems;
using Coolify.Resource.Manager.Models.Foundations.Systems;
using Coolify.Resource.Manager.Services.Foundations.Systems;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Systems
{
    public partial class SystemServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ISystemService systemService;

        public SystemServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.systemService = new SystemService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalSystemInfo CreateRandomExternalSystemInfo() =>
            new ExternalSystemInfo { Version = GetRandomString() };

        private static SystemInfo ConvertToSystemInfo(ExternalSystemInfo externalSystemInfo) =>
            new SystemInfo { Version = externalSystemInfo.Version };

        public static TheoryData<HttpStatusCode> DependencyValidationHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.BadRequest
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
