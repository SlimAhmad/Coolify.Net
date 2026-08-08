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
        public async Task ShouldThrowDependencyValidationExceptionOnAddMariaDbDatabaseIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new MariaDbDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MariaDbDatabase> addMariaDbDatabaseTask =
                this.databaseService.AddMariaDbDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addMariaDbDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddMariaDbDatabaseIfConflictErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new MariaDbDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MariaDbDatabase> addMariaDbDatabaseTask =
                this.databaseService.AddMariaDbDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addMariaDbDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddMariaDbDatabaseIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new MariaDbDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MariaDbDatabase> addMariaDbDatabaseTask =
                this.databaseService.AddMariaDbDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addMariaDbDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddMariaDbDatabaseIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new MariaDbDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MariaDbDatabase> addMariaDbDatabaseTask =
                this.databaseService.AddMariaDbDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addMariaDbDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddMariaDbDatabaseIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var someDatabase = new MariaDbDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MariaDbDatabase> addMariaDbDatabaseTask =
                this.databaseService.AddMariaDbDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addMariaDbDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddMariaDbDatabaseIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new MariaDbDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<MariaDbDatabase> addMariaDbDatabaseTask =
                this.databaseService.AddMariaDbDatabaseAsync(someDatabase);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addMariaDbDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMariaDbDatabaseAsync(It.IsAny<ExternalMariaDbDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
