// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.PrivateKeys;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Services.Foundations.PrivateKeys;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPrivateKeyService privateKeyService;

        public PrivateKeyServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.privateKeyService = new PrivateKeyService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalPrivateKey CreateRandomExternalPrivateKey() =>
            new ExternalPrivateKey
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                PrivateKeyValue = GetRandomString(),
                TeamId = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static PrivateKey CreateRandomPrivateKey() =>
            ConvertToPrivateKey(CreateRandomExternalPrivateKey());

        private static PrivateKey ConvertToPrivateKey(ExternalPrivateKey externalPrivateKey) =>
            new PrivateKey
            {
                Uuid = externalPrivateKey.Uuid,
                Name = externalPrivateKey.Name,
                Description = externalPrivateKey.Description,
                PrivateKeyValue = externalPrivateKey.PrivateKeyValue,
                TeamId = externalPrivateKey.TeamId,
                CreatedAt = externalPrivateKey.CreatedAt,
                UpdatedAt = externalPrivateKey.UpdatedAt
            };

        private static ExternalPrivateKey ConvertToExternalPrivateKey(PrivateKey privateKey) =>
            new ExternalPrivateKey
            {
                Uuid = privateKey.Uuid,
                Name = privateKey.Name,
                Description = privateKey.Description,
                PrivateKeyValue = privateKey.PrivateKeyValue,
                TeamId = privateKey.TeamId
            };

        private static bool IsSameExternalPrivateKey(ExternalPrivateKey actual, ExternalPrivateKey expected) =>
            actual.Uuid == expected.Uuid
            && actual.Name == expected.Name
            && actual.PrivateKeyValue == expected.PrivateKeyValue;

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
