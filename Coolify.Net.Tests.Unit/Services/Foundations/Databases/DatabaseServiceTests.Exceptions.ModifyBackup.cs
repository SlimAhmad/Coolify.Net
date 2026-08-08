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
        public async Task ShouldThrowDependencyValidationExceptionOnModifyBackupIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> modifyBackupTask =
                this.databaseService.ModifyBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(modifyBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyBackupIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> modifyBackupTask =
                this.databaseService.ModifyBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(modifyBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyBackupIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> modifyBackupTask =
                this.databaseService.ModifyBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(modifyBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnModifyBackupIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> modifyBackupTask =
                this.databaseService.ModifyBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(modifyBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyBackupIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DatabaseBackup> modifyBackupTask =
                this.databaseService.ModifyBackupAsync(someDatabaseUuid, someBackup);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(modifyBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyBackupIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup someBackup = CreateRandomDatabaseBackup();
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<DatabaseBackup> modifyBackupTask =
                this.databaseService.ModifyBackupAsync(someDatabaseUuid, someBackup);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(modifyBackupTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(someDatabaseUuid, someBackup.Uuid, It.IsAny<ExternalDatabaseBackup>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
