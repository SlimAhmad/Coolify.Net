// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Services.Foundations.Databases;
using Coolify.Resource.Manager.Services.Processings.Databases;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Databases
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
