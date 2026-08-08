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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllBackupsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<DatabaseBackup>> retrieveAllBackupsTask =
                this.databaseService.RetrieveAllBackupsAsync(someDatabaseUuid);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveAllBackupsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllBackupsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<DatabaseBackup>> retrieveAllBackupsTask =
                this.databaseService.RetrieveAllBackupsAsync(someDatabaseUuid);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveAllBackupsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllBackupsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<DatabaseBackup>> retrieveAllBackupsTask =
                this.databaseService.RetrieveAllBackupsAsync(someDatabaseUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllBackupsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllBackupsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<DatabaseBackup>> retrieveAllBackupsTask =
                this.databaseService.RetrieveAllBackupsAsync(someDatabaseUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllBackupsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllBackupsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<DatabaseBackup>> retrieveAllBackupsTask =
                this.databaseService.RetrieveAllBackupsAsync(someDatabaseUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllBackupsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllBackupsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(someDatabaseUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<DatabaseBackup>> retrieveAllBackupsTask =
                this.databaseService.RetrieveAllBackupsAsync(someDatabaseUuid);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(retrieveAllBackupsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
