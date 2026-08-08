// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Databases;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Processings.Databases.Exceptions;
using Coolify.Net.Services.Processings.Databases;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Databases
{
    public partial class DatabaseClientTests
    {
        private readonly Mock<IDatabaseProcessingService> databaseServiceMock;
        private readonly IDatabaseClient databaseClient;

        public DatabaseClientTests()
        {
            this.databaseServiceMock = new Mock<IDatabaseProcessingService>();

            this.databaseClient = new DatabaseClient(
                databaseProcessingService: this.databaseServiceMock.Object);
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

        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DatabaseProcessingValidationException("test", inner),
                new DatabaseProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DatabaseProcessingDependencyException("test", inner),
                new DatabaseProcessingServiceException("test", inner)
            };
        }
    }
}
