// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Databases;
using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Services.Foundations.Databases;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IDatabaseService databaseService;

        public DatabaseServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.databaseService = new DatabaseService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static ExternalDatabase CreateRandomExternalDatabase() =>
            new ExternalDatabase
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                EnvironmentUuid = GetRandomString(),
                EnvironmentName = GetRandomString(),
                Image = GetRandomString(),
                Status = GetRandomString(),
                PublicPort = 5432,
                IsPublic = true,
                DatabaseType = "postgresql",
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static Database ConvertToDatabase(ExternalDatabase externalDatabase) =>
            new Database
            {
                Uuid = externalDatabase.Uuid,
                Name = externalDatabase.Name,
                Description = externalDatabase.Description,
                ServerUuid = externalDatabase.ServerUuid,
                ProjectUuid = externalDatabase.ProjectUuid,
                EnvironmentUuid = externalDatabase.EnvironmentUuid,
                EnvironmentName = externalDatabase.EnvironmentName,
                Image = externalDatabase.Image,
                Status = externalDatabase.Status,
                PublicPort = externalDatabase.PublicPort,
                IsPublic = externalDatabase.IsPublic,
                DatabaseType = externalDatabase.DatabaseType,
                CreatedAt = externalDatabase.CreatedAt,
                UpdatedAt = externalDatabase.UpdatedAt
            };

        private static ExternalPostgreSqlDatabase CreateRandomExternalPostgreSqlDatabase() =>
            new ExternalPostgreSqlDatabase
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                PostgresUser = GetRandomString(),
                PostgresPassword = GetRandomString(),
                PostgresDb = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static PostgreSqlDatabase CreateRandomPostgreSqlDatabase() =>
            new PostgreSqlDatabase
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                PostgresUser = GetRandomString(),
                PostgresPassword = GetRandomString(),
                PostgresDb = GetRandomString()
            };

        private static ExternalDatabaseBackup CreateRandomExternalDatabaseBackup() =>
            new ExternalDatabaseBackup
            {
                Uuid = GetRandomString(),
                DatabaseUuid = GetRandomString(),
                FrequencyExpression = "0 0 * * *",
                Enabled = true,
                NumberOfBackupsLocally = 5,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static DatabaseBackup CreateRandomDatabaseBackup() =>
            new DatabaseBackup
            {
                Uuid = GetRandomString(),
                DatabaseUuid = GetRandomString(),
                FrequencyExpression = "0 0 * * *",
                Enabled = true,
                NumberOfBackupsLocally = 5
            };

        private static ExternalBackupExecution CreateRandomExternalBackupExecution() =>
            new ExternalBackupExecution
            {
                Uuid = GetRandomString(),
                BackupUuid = GetRandomString(),
                Status = GetRandomString(),
                Message = GetRandomString(),
                Filename = GetRandomString(),
                Size = 1024,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        public static TheoryData<HttpStatusCode> DependencyValidationHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.BadRequest,
                HttpStatusCode.Conflict
            };

        public static TheoryData<HttpStatusCode> CriticalDependencyHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
                HttpStatusCode.NotFound
            };

        public static TheoryData<HttpStatusCode> DependencyHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.TooManyRequests,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.InternalServerError
            };

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);
    }
}
