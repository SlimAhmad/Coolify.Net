// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddPostgreSqlWhenDatabaseIsNullAndLogItAsync()
        {
            PostgreSqlDatabase nullDatabase = null;

            var nullDatabaseException = new NullDatabaseException(message: "Database is null.");

            var expectedDatabaseValidationException =
                new DatabaseValidationException(
                    message: "Database validation error occurred, fix the errors and try again.",
                    innerException: nullDatabaseException);

            ValueTask<PostgreSqlDatabase> addDatabaseTask =
                this.databaseService.AddPostgreSqlDatabaseAsync(nullDatabase);

            DatabaseValidationException actualException =
                await Assert.ThrowsAsync<DatabaseValidationException>(addDatabaseTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDatabaseValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenDatabaseUuidIsInvalidAndLogItAsync(
            string invalidDatabaseUuid)
        {
            var invalidDatabaseException =
                new InvalidDatabaseException(
                    message: "Invalid database. Please fix the errors and try again.");

            invalidDatabaseException.UpsertDataList(key: "databaseUuid", value: "Text is required");

            var expectedDatabaseValidationException =
                new DatabaseValidationException(
                    message: "Database validation error occurred, fix the errors and try again.",
                    innerException: invalidDatabaseException);

            ValueTask<Database> retrieveByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(invalidDatabaseUuid);

            DatabaseValidationException actualException =
                await Assert.ThrowsAsync<DatabaseValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDatabaseValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddBackupWhenBackupIsNullAndLogItAsync()
        {
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup nullBackup = null;

            var nullDatabaseException = new NullDatabaseException(message: "Backup is null.");

            var expectedDatabaseValidationException =
                new DatabaseValidationException(
                    message: "Database validation error occurred, fix the errors and try again.",
                    innerException: nullDatabaseException);

            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseService.AddBackupAsync(someDatabaseUuid, nullBackup);

            DatabaseValidationException actualException =
                await Assert.ThrowsAsync<DatabaseValidationException>(addBackupTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDatabaseValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
