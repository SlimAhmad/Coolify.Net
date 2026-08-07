// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Models.Processings.Databases.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Databases
{
    public partial class DatabaseProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddPostgreSqlWhenDatabaseIsNullAndLogItAsync()
        {
            PostgreSqlDatabase nullDatabase = null;

            var nullDatabaseProcessingException =
                new NullDatabaseProcessingException(message: "Database is null.");

            var expectedDatabaseProcessingValidationException =
                new DatabaseProcessingValidationException(
                    message: "Database processing validation error occurred, fix the errors and try again.",
                    innerException: nullDatabaseProcessingException);

            ValueTask<PostgreSqlDatabase> addDatabaseTask =
                this.databaseProcessingService.AddPostgreSqlDatabaseAsync(nullDatabase);

            DatabaseProcessingValidationException actualException =
                await Assert.ThrowsAsync<DatabaseProcessingValidationException>(addDatabaseTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDatabaseProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenDatabaseUuidIsInvalidAndLogItAsync(
            string invalidDatabaseUuid)
        {
            var invalidDatabaseProcessingException =
                new InvalidDatabaseProcessingException(message: "Database uuid is invalid.");

            var expectedDatabaseProcessingValidationException =
                new DatabaseProcessingValidationException(
                    message: "Database processing validation error occurred, fix the errors and try again.",
                    innerException: invalidDatabaseProcessingException);

            ValueTask<Database> retrieveByUuidTask =
                this.databaseProcessingService.RetrieveDatabaseByUuidAsync(invalidDatabaseUuid);

            DatabaseProcessingValidationException actualException =
                await Assert.ThrowsAsync<DatabaseProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDatabaseProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddBackupWhenBackupIsNullAndLogItAsync()
        {
            string someDatabaseUuid = GetRandomString();
            DatabaseBackup nullBackup = null;

            var nullDatabaseProcessingException =
                new NullDatabaseProcessingException(message: "Backup is null.");

            var expectedDatabaseProcessingValidationException =
                new DatabaseProcessingValidationException(
                    message: "Database processing validation error occurred, fix the errors and try again.",
                    innerException: nullDatabaseProcessingException);

            ValueTask<DatabaseBackup> addBackupTask =
                this.databaseProcessingService.AddBackupAsync(someDatabaseUuid, nullBackup);

            DatabaseProcessingValidationException actualException =
                await Assert.ThrowsAsync<DatabaseProcessingValidationException>(addBackupTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedDatabaseProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
