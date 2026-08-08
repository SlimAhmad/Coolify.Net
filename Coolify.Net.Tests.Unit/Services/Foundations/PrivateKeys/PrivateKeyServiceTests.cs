// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys.Exceptions;
using Coolify.Net.Services.Foundations.PrivateKeys;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Foundations.PrivateKeys
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

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static PrivateKeyDependencyValidationException CreateInvalidPrivateKeyDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidPrivateKeyException = new InvalidPrivateKeyException(
                message: "Invalid private key.",
                innerException: httpRequestException);

            return new PrivateKeyDependencyValidationException(
                message: "Private key dependency validation error occurred, fix the errors and try again.",
                innerException: invalidPrivateKeyException);
        }

        private static PrivateKeyDependencyValidationException CreateAlreadyExistsPrivateKeyDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsPrivateKeyException = new AlreadyExistsPrivateKeyException(
                message: "Private key already exists.",
                innerException: httpRequestException);

            return new PrivateKeyDependencyValidationException(
                message: "Private key dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsPrivateKeyException);
        }

        private static PrivateKeyDependencyException CreateFailedPrivateKeyDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedPrivateKeyDependencyException = new FailedPrivateKeyDependencyException(
                message: "Failed private key dependency error occurred.",
                innerException: httpRequestException);

            return new PrivateKeyDependencyException(
                message: "Private key dependency error occurred, contact support.",
                innerException: failedPrivateKeyDependencyException);
        }

        private static PrivateKeyServiceException CreateFailedPrivateKeyServiceException(Exception exception)
        {
            var failedPrivateKeyServiceException = new FailedPrivateKeyServiceException(
                message: "Failed private key service error occurred.",
                innerException: exception);

            return new PrivateKeyServiceException(
                message: "Private key service error occurred, contact support.",
                innerException: failedPrivateKeyServiceException);
        }
    }
}
