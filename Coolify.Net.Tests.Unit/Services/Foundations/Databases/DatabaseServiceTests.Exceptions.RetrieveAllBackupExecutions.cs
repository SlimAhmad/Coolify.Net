// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllBackupExecutionsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            string someBackupUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<BackupExecution>> retrieveAllBackupExecutionsTask =
                this.databaseService.RetrieveAllBackupExecutionsAsync(someDatabaseUuid, someBackupUuid);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveAllBackupExecutionsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllBackupExecutionsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            string someBackupUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<BackupExecution>> retrieveAllBackupExecutionsTask =
                this.databaseService.RetrieveAllBackupExecutionsAsync(someDatabaseUuid, someBackupUuid);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveAllBackupExecutionsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllBackupExecutionsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            string someBackupUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<BackupExecution>> retrieveAllBackupExecutionsTask =
                this.databaseService.RetrieveAllBackupExecutionsAsync(someDatabaseUuid, someBackupUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllBackupExecutionsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllBackupExecutionsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            string someBackupUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<BackupExecution>> retrieveAllBackupExecutionsTask =
                this.databaseService.RetrieveAllBackupExecutionsAsync(someDatabaseUuid, someBackupUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllBackupExecutionsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllBackupExecutionsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            string someBackupUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<BackupExecution>> retrieveAllBackupExecutionsTask =
                this.databaseService.RetrieveAllBackupExecutionsAsync(someDatabaseUuid, someBackupUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllBackupExecutionsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllBackupExecutionsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            string someBackupUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<BackupExecution>> retrieveAllBackupExecutionsTask =
                this.databaseService.RetrieveAllBackupExecutionsAsync(someDatabaseUuid, someBackupUuid);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(retrieveAllBackupExecutionsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(someDatabaseUuid, someBackupUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
