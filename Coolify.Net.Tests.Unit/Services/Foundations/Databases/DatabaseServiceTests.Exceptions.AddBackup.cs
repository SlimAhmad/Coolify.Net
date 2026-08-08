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
        public async Task ShouldThrowDependencyValidationExceptionOnAddBackupIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddBackupIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddBackupIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddBackupIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddBackupIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddBackupIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, someBackup);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(someDatabaseUuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
