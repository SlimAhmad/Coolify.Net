// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Databases
{
    public partial class DatabaseProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllDatabasesAsync()
        {
            IEnumerable<Database> randomDatabases = Enumerable.Range(0, 3).Select(_ => CreateRandomDatabase());

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDatabases);

            IEnumerable<Database> actualDatabases = await this.databaseProcessingService.RetrieveAllDatabasesAsync();

            actualDatabases.Should().BeEquivalentTo(randomDatabases);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveDatabaseByUuidAsync()
        {
            Database randomDatabase = CreateRandomDatabase();
            string inputDatabaseUuid = randomDatabase.Uuid;

            this.databaseServiceMock
                .Setup(service => service.RetrieveDatabaseByUuidAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDatabase);

            Database actualDatabase = await this.databaseProcessingService.RetrieveDatabaseByUuidAsync(inputDatabaseUuid);

            actualDatabase.Should().BeEquivalentTo(randomDatabase);

            this.databaseServiceMock.Verify(service =>
                service.RetrieveDatabaseByUuidAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPostgreSqlDatabaseAsync()
        {
            PostgreSqlDatabase inputDatabase = CreateRandomPostgreSqlDatabase();
            PostgreSqlDatabase randomDatabase = CreateRandomPostgreSqlDatabase();

            this.databaseServiceMock
                .Setup(service => service.AddPostgreSqlDatabaseAsync(inputDatabase, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDatabase);

            PostgreSqlDatabase actualDatabase =
                await this.databaseProcessingService.AddPostgreSqlDatabaseAsync(inputDatabase);

            actualDatabase.Should().BeEquivalentTo(randomDatabase);

            this.databaseServiceMock.Verify(service =>
                service.AddPostgreSqlDatabaseAsync(inputDatabase, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyDatabaseAsync()
        {
            Database inputDatabase = CreateRandomDatabase();
            Database randomDatabase = CreateRandomDatabase();

            this.databaseServiceMock
                .Setup(service => service.ModifyDatabaseAsync(inputDatabase, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDatabase);

            Database actualDatabase = await this.databaseProcessingService.ModifyDatabaseAsync(inputDatabase);

            actualDatabase.Should().BeEquivalentTo(randomDatabase);

            this.databaseServiceMock.Verify(
                service => service.ModifyDatabaseAsync(inputDatabase, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.databaseServiceMock
                .Setup(service => service.RemoveDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseProcessingService.RemoveDatabaseAsync(inputDatabaseUuid);

            this.databaseServiceMock.Verify(
                service => service.RemoveDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllBackupsAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            IEnumerable<DatabaseBackup> randomBackups = Enumerable.Range(0, 3).Select(_ => CreateRandomDatabaseBackup());

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllBackupsAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomBackups);

            IEnumerable<DatabaseBackup> actualBackups =
                await this.databaseProcessingService.RetrieveAllBackupsAsync(inputDatabaseUuid);

            actualBackups.Should().BeEquivalentTo(randomBackups);

            this.databaseServiceMock.Verify(service =>
                service.RetrieveAllBackupsAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddBackupAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            DatabaseBackup inputBackup = CreateRandomDatabaseBackup();
            DatabaseBackup randomBackup = CreateRandomDatabaseBackup();

            this.databaseServiceMock
                .Setup(service => service.AddBackupAsync(inputDatabaseUuid, inputBackup, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomBackup);

            DatabaseBackup actualBackup =
                await this.databaseProcessingService.AddBackupAsync(inputDatabaseUuid, inputBackup);

            actualBackup.Should().BeEquivalentTo(randomBackup);

            this.databaseServiceMock.Verify(service =>
                service.AddBackupAsync(inputDatabaseUuid, inputBackup, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyBackupAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            DatabaseBackup inputBackup = CreateRandomDatabaseBackup();
            DatabaseBackup randomBackup = CreateRandomDatabaseBackup();

            this.databaseServiceMock
                .Setup(service => service.ModifyBackupAsync(inputDatabaseUuid, inputBackup, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomBackup);

            DatabaseBackup actualBackup =
                await this.databaseProcessingService.ModifyBackupAsync(inputDatabaseUuid, inputBackup);

            actualBackup.Should().BeEquivalentTo(randomBackup);

            this.databaseServiceMock.Verify(service =>
                service.ModifyBackupAsync(inputDatabaseUuid, inputBackup, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveBackupAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            string inputBackupUuid = GetRandomString();

            this.databaseServiceMock
                .Setup(service => service.RemoveBackupAsync(inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseProcessingService.RemoveBackupAsync(inputDatabaseUuid, inputBackupUuid);

            this.databaseServiceMock.Verify(service =>
                service.RemoveBackupAsync(inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllBackupExecutionsAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            string inputBackupUuid = GetRandomString();

            IEnumerable<BackupExecution> randomExecutions =
                Enumerable.Range(0, 3).Select(_ => CreateRandomBackupExecution());

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllBackupExecutionsAsync(
                    inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExecutions);

            IEnumerable<BackupExecution> actualExecutions =
                await this.databaseProcessingService.RetrieveAllBackupExecutionsAsync(inputDatabaseUuid, inputBackupUuid);

            actualExecutions.Should().BeEquivalentTo(randomExecutions);

            this.databaseServiceMock.Verify(service =>
                service.RetrieveAllBackupExecutionsAsync(inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveBackupExecutionAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            string inputBackupUuid = GetRandomString();
            string inputExecutionUuid = GetRandomString();

            this.databaseServiceMock
                .Setup(service => service.RemoveBackupExecutionAsync(
                    inputDatabaseUuid, inputBackupUuid, inputExecutionUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseProcessingService.RemoveBackupExecutionAsync(
                inputDatabaseUuid, inputBackupUuid, inputExecutionUuid);

            this.databaseServiceMock.Verify(service =>
                service.RemoveBackupExecutionAsync(
                    inputDatabaseUuid, inputBackupUuid, inputExecutionUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStartDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.databaseServiceMock
                .Setup(service => service.StartDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseProcessingService.StartDatabaseAsync(inputDatabaseUuid);

            this.databaseServiceMock.Verify(
                service => service.StartDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStopDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.databaseServiceMock
                .Setup(service => service.StopDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseProcessingService.StopDatabaseAsync(inputDatabaseUuid);

            this.databaseServiceMock.Verify(
                service => service.StopDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRestartDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.databaseServiceMock
                .Setup(service => service.RestartDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseProcessingService.RestartDatabaseAsync(inputDatabaseUuid);

            this.databaseServiceMock.Verify(
                service => service.RestartDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
