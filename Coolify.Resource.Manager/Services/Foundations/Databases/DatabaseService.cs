// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Databases;
using Coolify.Resource.Manager.Models.Foundations.Databases;

namespace Coolify.Resource.Manager.Services.Foundations.Databases
{
    public partial class DatabaseService : IDatabaseService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public DatabaseService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Database>> RetrieveAllDatabasesAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalDatabase> externalDatabases =
                        await this.coolifyApiBroker.GetAllDatabasesAsync(cancellationToken);

                    return externalDatabases.Select(ConvertToDatabase);
                });

        public ValueTask<Database> RetrieveDatabaseByUuidAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    ExternalDatabase externalDatabase =
                        await this.coolifyApiBroker.GetDatabaseByUuidAsync(databaseUuid, cancellationToken);

                    return ConvertToDatabase(externalDatabase);
                });

        public ValueTask<PostgreSqlDatabase> AddPostgreSqlDatabaseAsync(
            PostgreSqlDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalPostgreSqlDatabase externalDatabase = ConvertToExternalPostgreSqlDatabase(database);

                    ExternalPostgreSqlDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostPostgreSqlDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToPostgreSqlDatabase(returnedExternalDatabase);
                });

        public ValueTask<MySqlDatabase> AddMySqlDatabaseAsync(
            MySqlDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalMySqlDatabase externalDatabase = ConvertToExternalMySqlDatabase(database);

                    ExternalMySqlDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostMySqlDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToMySqlDatabase(returnedExternalDatabase);
                });

        public ValueTask<MariaDbDatabase> AddMariaDbDatabaseAsync(
            MariaDbDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalMariaDbDatabase externalDatabase = ConvertToExternalMariaDbDatabase(database);

                    ExternalMariaDbDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostMariaDbDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToMariaDbDatabase(returnedExternalDatabase);
                });

        public ValueTask<MongoDbDatabase> AddMongoDbDatabaseAsync(
            MongoDbDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalMongoDbDatabase externalDatabase = ConvertToExternalMongoDbDatabase(database);

                    ExternalMongoDbDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostMongoDbDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToMongoDbDatabase(returnedExternalDatabase);
                });

        public ValueTask<RedisDatabase> AddRedisDatabaseAsync(
            RedisDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalRedisDatabase externalDatabase = ConvertToExternalRedisDatabase(database);

                    ExternalRedisDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostRedisDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToRedisDatabase(returnedExternalDatabase);
                });

        public ValueTask<ClickHouseDatabase> AddClickHouseDatabaseAsync(
            ClickHouseDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalClickHouseDatabase externalDatabase = ConvertToExternalClickHouseDatabase(database);

                    ExternalClickHouseDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostClickHouseDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToClickHouseDatabase(returnedExternalDatabase);
                });

        public ValueTask<DragonflyDatabase> AddDragonflyDatabaseAsync(
            DragonflyDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalDragonflyDatabase externalDatabase = ConvertToExternalDragonflyDatabase(database);

                    ExternalDragonflyDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostDragonflyDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToDragonflyDatabase(returnedExternalDatabase);
                });

        public ValueTask<KeyDbDatabase> AddKeyDbDatabaseAsync(
            KeyDbDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalKeyDbDatabase externalDatabase = ConvertToExternalKeyDbDatabase(database);

                    ExternalKeyDbDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PostKeyDbDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToKeyDbDatabase(returnedExternalDatabase);
                });

        public ValueTask<Database> ModifyDatabaseAsync(
            Database database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    ExternalDatabase externalDatabase = ConvertToExternalDatabase(database);

                    ExternalDatabase returnedExternalDatabase =
                        await this.coolifyApiBroker.PatchDatabaseAsync(externalDatabase, cancellationToken);

                    return ConvertToDatabase(returnedExternalDatabase);
                });

        public ValueTask RemoveDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    await this.coolifyApiBroker.DeleteDatabaseAsync(databaseUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<DatabaseBackup>> RetrieveAllBackupsAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    IEnumerable<ExternalDatabaseBackup> externalBackups =
                        await this.coolifyApiBroker.GetDatabaseBackupsAsync(databaseUuid, cancellationToken);

                    return externalBackups.Select(ConvertToDatabaseBackup);
                });

        public ValueTask<DatabaseBackup> AddBackupAsync(
            string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupIsNotNull(backup);

                    ExternalDatabaseBackup externalBackup = ConvertToExternalDatabaseBackup(backup);

                    ExternalDatabaseBackup returnedExternalBackup =
                        await this.coolifyApiBroker.PostDatabaseBackupAsync(databaseUuid, externalBackup, cancellationToken);

                    return ConvertToDatabaseBackup(returnedExternalBackup);
                });

        public ValueTask<DatabaseBackup> ModifyBackupAsync(
            string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupIsNotNull(backup);

                    ExternalDatabaseBackup externalBackup = ConvertToExternalDatabaseBackup(backup);

                    ExternalDatabaseBackup returnedExternalBackup =
                        await this.coolifyApiBroker.PatchDatabaseBackupAsync(
                            databaseUuid, backup.Uuid, externalBackup, cancellationToken);

                    return ConvertToDatabaseBackup(returnedExternalBackup);
                });

        public ValueTask RemoveBackupAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupUuid(backupUuid);

                    await this.coolifyApiBroker.DeleteDatabaseBackupAsync(databaseUuid, backupUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<BackupExecution>> RetrieveAllBackupExecutionsAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupUuid(backupUuid);

                    IEnumerable<ExternalBackupExecution> externalExecutions =
                        await this.coolifyApiBroker.GetDatabaseBackupExecutionsAsync(
                            databaseUuid, backupUuid, cancellationToken);

                    return externalExecutions.Select(ConvertToBackupExecution);
                });

        public ValueTask RemoveBackupExecutionAsync(
            string databaseUuid, string backupUuid, string executionUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupUuid(backupUuid);
                    ValidateExecutionUuid(executionUuid);

                    await this.coolifyApiBroker.DeleteDatabaseBackupExecutionAsync(
                        databaseUuid, backupUuid, executionUuid, cancellationToken);
                });

        public ValueTask StartDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    await this.coolifyApiBroker.PostDatabaseStartAsync(databaseUuid, cancellationToken);
                });

        public ValueTask StopDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    await this.coolifyApiBroker.PostDatabaseStopAsync(databaseUuid, cancellationToken);
                });

        public ValueTask RestartDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    await this.coolifyApiBroker.PostDatabaseRestartAsync(databaseUuid, cancellationToken);
                });

        // ---- Conversion helpers ----

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

        private static ExternalDatabase ConvertToExternalDatabase(Database database) =>
            new ExternalDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                DatabaseType = database.DatabaseType
            };

        private static PostgreSqlDatabase ConvertToPostgreSqlDatabase(ExternalPostgreSqlDatabase external)
        {
            PostgreSqlDatabase database = MapBase<PostgreSqlDatabase>(external);
            database.PostgresUser = external.PostgresUser;
            database.PostgresPassword = external.PostgresPassword;
            database.PostgresDb = external.PostgresDb;
            database.PostgresInitdbArgs = external.PostgresInitdbArgs;
            database.PostgresHostAuthMethod = external.PostgresHostAuthMethod;
            database.PostgresConf = external.PostgresConf;
            return database;
        }

        private static ExternalPostgreSqlDatabase ConvertToExternalPostgreSqlDatabase(PostgreSqlDatabase database) =>
            new ExternalPostgreSqlDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                PostgresUser = database.PostgresUser,
                PostgresPassword = database.PostgresPassword,
                PostgresDb = database.PostgresDb,
                PostgresInitdbArgs = database.PostgresInitdbArgs,
                PostgresHostAuthMethod = database.PostgresHostAuthMethod,
                PostgresConf = database.PostgresConf
            };

        private static MySqlDatabase ConvertToMySqlDatabase(ExternalMySqlDatabase external)
        {
            MySqlDatabase database = MapBase<MySqlDatabase>(external);
            database.MysqlRootPassword = external.MysqlRootPassword;
            database.MysqlUser = external.MysqlUser;
            database.MysqlPassword = external.MysqlPassword;
            database.MysqlDatabase = external.MysqlDatabase;
            database.MysqlConf = external.MysqlConf;
            return database;
        }

        private static ExternalMySqlDatabase ConvertToExternalMySqlDatabase(MySqlDatabase database) =>
            new ExternalMySqlDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                MysqlRootPassword = database.MysqlRootPassword,
                MysqlUser = database.MysqlUser,
                MysqlPassword = database.MysqlPassword,
                MysqlDatabase = database.MysqlDatabase,
                MysqlConf = database.MysqlConf
            };

        private static MariaDbDatabase ConvertToMariaDbDatabase(ExternalMariaDbDatabase external)
        {
            MariaDbDatabase database = MapBase<MariaDbDatabase>(external);
            database.MariadbRootPassword = external.MariadbRootPassword;
            database.MariadbUser = external.MariadbUser;
            database.MariadbPassword = external.MariadbPassword;
            database.MariadbDatabase = external.MariadbDatabase;
            database.MariadbConf = external.MariadbConf;
            return database;
        }

        private static ExternalMariaDbDatabase ConvertToExternalMariaDbDatabase(MariaDbDatabase database) =>
            new ExternalMariaDbDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                MariadbRootPassword = database.MariadbRootPassword,
                MariadbUser = database.MariadbUser,
                MariadbPassword = database.MariadbPassword,
                MariadbDatabase = database.MariadbDatabase,
                MariadbConf = database.MariadbConf
            };

        private static MongoDbDatabase ConvertToMongoDbDatabase(ExternalMongoDbDatabase external)
        {
            MongoDbDatabase database = MapBase<MongoDbDatabase>(external);
            database.MongoInitdbRootUsername = external.MongoInitdbRootUsername;
            database.MongoInitdbRootPassword = external.MongoInitdbRootPassword;
            database.MongoInitdbDatabase = external.MongoInitdbDatabase;
            database.MongoConf = external.MongoConf;
            return database;
        }

        private static ExternalMongoDbDatabase ConvertToExternalMongoDbDatabase(MongoDbDatabase database) =>
            new ExternalMongoDbDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                MongoInitdbRootUsername = database.MongoInitdbRootUsername,
                MongoInitdbRootPassword = database.MongoInitdbRootPassword,
                MongoInitdbDatabase = database.MongoInitdbDatabase,
                MongoConf = database.MongoConf
            };

        private static RedisDatabase ConvertToRedisDatabase(ExternalRedisDatabase external)
        {
            RedisDatabase database = MapBase<RedisDatabase>(external);
            database.RedisPassword = external.RedisPassword;
            database.RedisConf = external.RedisConf;
            return database;
        }

        private static ExternalRedisDatabase ConvertToExternalRedisDatabase(RedisDatabase database) =>
            new ExternalRedisDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                RedisPassword = database.RedisPassword,
                RedisConf = database.RedisConf
            };

        private static ClickHouseDatabase ConvertToClickHouseDatabase(ExternalClickHouseDatabase external)
        {
            ClickHouseDatabase database = MapBase<ClickHouseDatabase>(external);
            database.ClickhouseAdminUser = external.ClickhouseAdminUser;
            database.ClickhouseAdminPassword = external.ClickhouseAdminPassword;
            return database;
        }

        private static ExternalClickHouseDatabase ConvertToExternalClickHouseDatabase(ClickHouseDatabase database) =>
            new ExternalClickHouseDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                ClickhouseAdminUser = database.ClickhouseAdminUser,
                ClickhouseAdminPassword = database.ClickhouseAdminPassword
            };

        private static DragonflyDatabase ConvertToDragonflyDatabase(ExternalDragonflyDatabase external)
        {
            DragonflyDatabase database = MapBase<DragonflyDatabase>(external);
            database.DragonflyPassword = external.DragonflyPassword;
            return database;
        }

        private static ExternalDragonflyDatabase ConvertToExternalDragonflyDatabase(DragonflyDatabase database) =>
            new ExternalDragonflyDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                DragonflyPassword = database.DragonflyPassword
            };

        private static KeyDbDatabase ConvertToKeyDbDatabase(ExternalKeyDbDatabase external)
        {
            KeyDbDatabase database = MapBase<KeyDbDatabase>(external);
            database.KeydbPassword = external.KeydbPassword;
            database.KeydbConf = external.KeydbConf;
            return database;
        }

        private static ExternalKeyDbDatabase ConvertToExternalKeyDbDatabase(KeyDbDatabase database) =>
            new ExternalKeyDbDatabase
            {
                Uuid = database.Uuid,
                Name = database.Name,
                Description = database.Description,
                ServerUuid = database.ServerUuid,
                ProjectUuid = database.ProjectUuid,
                EnvironmentUuid = database.EnvironmentUuid,
                EnvironmentName = database.EnvironmentName,
                Image = database.Image,
                PublicPort = database.PublicPort,
                IsPublic = database.IsPublic,
                KeydbPassword = database.KeydbPassword,
                KeydbConf = database.KeydbConf
            };

        private static TDatabase MapBase<TDatabase>(ExternalDatabase external) where TDatabase : Database, new() =>
            new TDatabase
            {
                Uuid = external.Uuid,
                Name = external.Name,
                Description = external.Description,
                ServerUuid = external.ServerUuid,
                ProjectUuid = external.ProjectUuid,
                EnvironmentUuid = external.EnvironmentUuid,
                EnvironmentName = external.EnvironmentName,
                Image = external.Image,
                Status = external.Status,
                PublicPort = external.PublicPort,
                IsPublic = external.IsPublic,
                DatabaseType = external.DatabaseType,
                CreatedAt = external.CreatedAt,
                UpdatedAt = external.UpdatedAt
            };

        private static DatabaseBackup ConvertToDatabaseBackup(ExternalDatabaseBackup external) =>
            new DatabaseBackup
            {
                Uuid = external.Uuid,
                DatabaseUuid = external.DatabaseUuid,
                FrequencyExpression = external.FrequencyExpression,
                Enabled = external.Enabled,
                NumberOfBackupsLocally = external.NumberOfBackupsLocally,
                S3StorageUuid = external.S3StorageUuid,
                CreatedAt = external.CreatedAt,
                UpdatedAt = external.UpdatedAt
            };

        private static ExternalDatabaseBackup ConvertToExternalDatabaseBackup(DatabaseBackup backup) =>
            new ExternalDatabaseBackup
            {
                Uuid = backup.Uuid,
                DatabaseUuid = backup.DatabaseUuid,
                FrequencyExpression = backup.FrequencyExpression,
                Enabled = backup.Enabled,
                NumberOfBackupsLocally = backup.NumberOfBackupsLocally,
                S3StorageUuid = backup.S3StorageUuid
            };

        private static BackupExecution ConvertToBackupExecution(ExternalBackupExecution external) =>
            new BackupExecution
            {
                Uuid = external.Uuid,
                BackupUuid = external.BackupUuid,
                Status = external.Status,
                Message = external.Message,
                Filename = external.Filename,
                Size = external.Size,
                CreatedAt = external.CreatedAt,
                UpdatedAt = external.UpdatedAt
            };
    }
}
