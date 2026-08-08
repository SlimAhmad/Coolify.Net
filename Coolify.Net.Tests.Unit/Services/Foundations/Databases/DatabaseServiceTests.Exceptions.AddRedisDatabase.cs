// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Databases;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddRedisDatabaseIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new RedisDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<RedisDatabase> addRedisDatabaseTask =
                this.databaseService.AddRedisDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addRedisDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddRedisDatabaseIfConflictErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new RedisDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<RedisDatabase> addRedisDatabaseTask =
                this.databaseService.AddRedisDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addRedisDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddRedisDatabaseIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new RedisDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<RedisDatabase> addRedisDatabaseTask =
                this.databaseService.AddRedisDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addRedisDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddRedisDatabaseIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new RedisDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<RedisDatabase> addRedisDatabaseTask =
                this.databaseService.AddRedisDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addRedisDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddRedisDatabaseIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var someDatabase = new RedisDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<RedisDatabase> addRedisDatabaseTask =
                this.databaseService.AddRedisDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addRedisDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddRedisDatabaseIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new RedisDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<RedisDatabase> addRedisDatabaseTask =
                this.databaseService.AddRedisDatabaseAsync(someDatabase);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addRedisDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostRedisDatabaseAsync(It.IsAny<ExternalRedisDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
