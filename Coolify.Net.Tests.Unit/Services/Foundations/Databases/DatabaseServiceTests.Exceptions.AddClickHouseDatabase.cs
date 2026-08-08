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
        public async Task ShouldThrowDependencyValidationExceptionOnAddClickHouseDatabaseIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new ClickHouseDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<ClickHouseDatabase> addClickHouseDatabaseTask =
                this.databaseService.AddClickHouseDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addClickHouseDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddClickHouseDatabaseIfConflictErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new ClickHouseDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<ClickHouseDatabase> addClickHouseDatabaseTask =
                this.databaseService.AddClickHouseDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addClickHouseDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddClickHouseDatabaseIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new ClickHouseDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<ClickHouseDatabase> addClickHouseDatabaseTask =
                this.databaseService.AddClickHouseDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addClickHouseDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddClickHouseDatabaseIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new ClickHouseDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<ClickHouseDatabase> addClickHouseDatabaseTask =
                this.databaseService.AddClickHouseDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addClickHouseDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddClickHouseDatabaseIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var someDatabase = new ClickHouseDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<ClickHouseDatabase> addClickHouseDatabaseTask =
                this.databaseService.AddClickHouseDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addClickHouseDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddClickHouseDatabaseIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new ClickHouseDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<ClickHouseDatabase> addClickHouseDatabaseTask =
                this.databaseService.AddClickHouseDatabaseAsync(someDatabase);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addClickHouseDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostClickHouseDatabaseAsync(It.IsAny<ExternalClickHouseDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
