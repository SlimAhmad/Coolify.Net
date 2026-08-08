// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Services.Foundations.Databases;
using Coolify.Net.Services.Processings.Databases;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Databases
{
    public partial class DatabaseProcessingServiceTests
    {
        private readonly Mock<IDatabaseService> databaseServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IDatabaseProcessingService databaseProcessingService;

        public DatabaseProcessingServiceTests()
        {
            this.databaseServiceMock = new Mock<IDatabaseService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.databaseProcessingService = new DatabaseProcessingService(
                databaseService: this.databaseServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Database CreateRandomDatabase() =>
            new Database { Uuid = GetRandomString(), Name = GetRandomString() };

        private static PostgreSqlDatabase CreateRandomPostgreSqlDatabase() =>
            new PostgreSqlDatabase { Uuid = GetRandomString(), Name = GetRandomString() };

        private static DatabaseBackup CreateRandomDatabaseBackup() =>
            new DatabaseBackup { Uuid = GetRandomString(), DatabaseUuid = GetRandomString() };

        private static BackupExecution CreateRandomBackupExecution() =>
            new BackupExecution { Uuid = GetRandomString(), BackupUuid = GetRandomString() };
    }
}
