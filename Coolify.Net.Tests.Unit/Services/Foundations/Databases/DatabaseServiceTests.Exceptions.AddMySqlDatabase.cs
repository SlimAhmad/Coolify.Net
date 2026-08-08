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
        public async Task ShouldThrowDependencyValidationExceptionOnAddMySqlDatabaseIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new MySqlDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MySqlDatabase> addMySqlDatabaseTask =
                this.databaseService.AddMySqlDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addMySqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddMySqlDatabaseIfConflictErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new MySqlDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MySqlDatabase> addMySqlDatabaseTask =
                this.databaseService.AddMySqlDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addMySqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddMySqlDatabaseIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new MySqlDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MySqlDatabase> addMySqlDatabaseTask =
                this.databaseService.AddMySqlDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addMySqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddMySqlDatabaseIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new MySqlDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MySqlDatabase> addMySqlDatabaseTask =
                this.databaseService.AddMySqlDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addMySqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddMySqlDatabaseIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var someDatabase = new MySqlDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<MySqlDatabase> addMySqlDatabaseTask =
                this.databaseService.AddMySqlDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addMySqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddMySqlDatabaseIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new MySqlDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<MySqlDatabase> addMySqlDatabaseTask =
                this.databaseService.AddMySqlDatabaseAsync(someDatabase);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addMySqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostMySqlDatabaseAsync(It.IsAny<ExternalMySqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
