// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using System.Linq.Expressions;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Databases;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;
using Coolify.Net.Services.Foundations.Databases;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
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

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static DatabaseDependencyValidationException CreateInvalidDatabaseDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidDatabaseException = new InvalidDatabaseException(
                message: "Invalid database.",
                innerException: httpRequestException);

            return new DatabaseDependencyValidationException(
                message: "Database dependency validation error occurred, fix the errors and try again.",
                innerException: invalidDatabaseException);
        }

        private static DatabaseDependencyValidationException CreateAlreadyExistsDatabaseDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsDatabaseException = new AlreadyExistsDatabaseException(
                message: "Database already exists.",
                innerException: httpRequestException);

            return new DatabaseDependencyValidationException(
                message: "Database dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsDatabaseException);
        }

        private static DatabaseDependencyException CreateFailedDatabaseDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedDatabaseDependencyException = new FailedDatabaseDependencyException(
                message: "Failed database dependency error occurred.",
                innerException: httpRequestException);

            return new DatabaseDependencyException(
                message: "Database dependency error occurred, contact support.",
                innerException: failedDatabaseDependencyException);
        }

        private static DatabaseServiceException CreateFailedDatabaseServiceException(Exception exception)
        {
            var failedDatabaseServiceException = new FailedDatabaseServiceException(
                message: "Failed database service error occurred.",
                innerException: exception);

            return new DatabaseServiceException(
                message: "Database service error occurred, contact support.",
                innerException: failedDatabaseServiceException);
        }
    }
}
