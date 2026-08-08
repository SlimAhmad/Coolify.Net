// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Databases;
using Coolify.Net.Models.Foundations.Databases;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllDatabasesAsync()
        {
            List<ExternalDatabase> randomExternalDatabases =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalDatabase()).ToList();

            IEnumerable<Database> expectedDatabases = randomExternalDatabases.Select(ConvertToDatabase);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalDatabases);

            IEnumerable<Database> actualDatabases = await this.databaseService.RetrieveAllDatabasesAsync();

            actualDatabases.Should().BeEquivalentTo(expectedDatabases);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveDatabaseByUuidAsync()
        {
            ExternalDatabase randomExternalDatabase = CreateRandomExternalDatabase();
            string inputDatabaseUuid = randomExternalDatabase.Uuid;
            Database expectedDatabase = ConvertToDatabase(randomExternalDatabase);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalDatabase);

            Database actualDatabase = await this.databaseService.RetrieveDatabaseByUuidAsync(inputDatabaseUuid);

            actualDatabase.Should().BeEquivalentTo(expectedDatabase);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPostgreSqlDatabaseAsync()
        {
            PostgreSqlDatabase inputDatabase = CreateRandomPostgreSqlDatabase();
            ExternalPostgreSqlDatabase returnedExternalDatabase = CreateRandomExternalPostgreSqlDatabase();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPostgreSqlDatabaseAsync(
                    It.IsAny<ExternalPostgreSqlDatabase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalDatabase);

            PostgreSqlDatabase actualDatabase = await this.databaseService.AddPostgreSqlDatabaseAsync(inputDatabase);

            actualDatabase.Uuid.Should().Be(returnedExternalDatabase.Uuid);
            actualDatabase.PostgresUser.Should().Be(returnedExternalDatabase.PostgresUser);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPostgreSqlDatabaseAsync(It.IsAny<ExternalPostgreSqlDatabase>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyDatabaseAsync()
        {
            Database inputDatabase = ConvertToDatabase(CreateRandomExternalDatabase());
            ExternalDatabase returnedExternalDatabase = CreateRandomExternalDatabase();
            Database expectedDatabase = ConvertToDatabase(returnedExternalDatabase);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseAsync(It.IsAny<ExternalDatabase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalDatabase);

            Database actualDatabase = await this.databaseService.ModifyDatabaseAsync(inputDatabase);

            actualDatabase.Should().BeEquivalentTo(expectedDatabase);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseAsync(It.IsAny<ExternalDatabase>(), It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseService.RemoveDatabaseAsync(inputDatabaseUuid);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteDatabaseAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllBackupsAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            List<ExternalDatabaseBackup> randomExternalBackups =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalDatabaseBackup()).ToList();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupsAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalBackups);

            IEnumerable<DatabaseBackup> actualBackups =
                await this.databaseService.RetrieveAllBackupsAsync(inputDatabaseUuid);

            actualBackups.Should().HaveCount(3);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupsAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddBackupAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            DatabaseBackup inputBackup = CreateRandomDatabaseBackup();
            ExternalDatabaseBackup returnedExternalBackup = CreateRandomExternalDatabaseBackup();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseBackupAsync(
                    inputDatabaseUuid, It.IsAny<ExternalDatabaseBackup>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalBackup);

            DatabaseBackup actualBackup = await this.databaseService.AddBackupAsync(inputDatabaseUuid, inputBackup);

            actualBackup.Uuid.Should().Be(returnedExternalBackup.Uuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDatabaseBackupAsync(
                    inputDatabaseUuid, It.IsAny<ExternalDatabaseBackup>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyBackupAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            DatabaseBackup inputBackup = CreateRandomDatabaseBackup();
            ExternalDatabaseBackup returnedExternalBackup = CreateRandomExternalDatabaseBackup();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchDatabaseBackupAsync(
                    inputDatabaseUuid, inputBackup.Uuid, It.IsAny<ExternalDatabaseBackup>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalBackup);

            DatabaseBackup actualBackup = await this.databaseService.ModifyBackupAsync(inputDatabaseUuid, inputBackup);

            actualBackup.Uuid.Should().Be(returnedExternalBackup.Uuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchDatabaseBackupAsync(
                    inputDatabaseUuid, inputBackup.Uuid, It.IsAny<ExternalDatabaseBackup>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveBackupAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            string inputBackupUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteDatabaseBackupAsync(
                    inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseService.RemoveBackupAsync(inputDatabaseUuid, inputBackupUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteDatabaseBackupAsync(inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllBackupExecutionsAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            string inputBackupUuid = GetRandomString();

            List<ExternalBackupExecution> randomExecutions =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalBackupExecution()).ToList();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseBackupExecutionsAsync(
                    inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExecutions);

            IEnumerable<BackupExecution> actualExecutions =
                await this.databaseService.RetrieveAllBackupExecutionsAsync(inputDatabaseUuid, inputBackupUuid);

            actualExecutions.Should().HaveCount(3);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseBackupExecutionsAsync(inputDatabaseUuid, inputBackupUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveBackupExecutionAsync()
        {
            string inputDatabaseUuid = GetRandomString();
            string inputBackupUuid = GetRandomString();
            string inputExecutionUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteDatabaseBackupExecutionAsync(
                    inputDatabaseUuid, inputBackupUuid, inputExecutionUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseService.RemoveBackupExecutionAsync(
                inputDatabaseUuid, inputBackupUuid, inputExecutionUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteDatabaseBackupExecutionAsync(
                    inputDatabaseUuid, inputBackupUuid, inputExecutionUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStartDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseStartAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseService.StartDatabaseAsync(inputDatabaseUuid);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostDatabaseStartAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStopDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseStopAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseService.StopDatabaseAsync(inputDatabaseUuid);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostDatabaseStopAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRestartDatabaseAsync()
        {
            string inputDatabaseUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDatabaseRestartAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.databaseService.RestartDatabaseAsync(inputDatabaseUuid);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostDatabaseRestartAsync(inputDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
