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
        public async Task ShouldThrowDependencyValidationExceptionOnAddPostgreSqlDatabaseIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            PostgreSqlDatabase someDatabase = CreateRandomPostgreSqlDatabase();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PostgreSqlDatabase> addPostgreSqlDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addPostgreSqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddPostgreSqlDatabaseIfConflictErrorOccursAndLogItAsync()
        {
            // given
            PostgreSqlDatabase someDatabase = CreateRandomPostgreSqlDatabase();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PostgreSqlDatabase> addPostgreSqlDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addPostgreSqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddPostgreSqlDatabaseIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            PostgreSqlDatabase someDatabase = CreateRandomPostgreSqlDatabase();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PostgreSqlDatabase> addPostgreSqlDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addPostgreSqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddPostgreSqlDatabaseIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            PostgreSqlDatabase someDatabase = CreateRandomPostgreSqlDatabase();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PostgreSqlDatabase> addPostgreSqlDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addPostgreSqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddPostgreSqlDatabaseIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            PostgreSqlDatabase someDatabase = CreateRandomPostgreSqlDatabase();
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<PostgreSqlDatabase> addPostgreSqlDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addPostgreSqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddPostgreSqlDatabaseIfServiceErrorOccursAndLogItAsync()
        {
            // given
            PostgreSqlDatabase someDatabase = CreateRandomPostgreSqlDatabase();
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<PostgreSqlDatabase> addPostgreSqlDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(someDatabase);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addPostgreSqlDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
