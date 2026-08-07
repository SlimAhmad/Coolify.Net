// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Services.Foundations.Databases;

namespace Coolify.Resource.Manager.Services.Processings.Databases
{
    public partial class DatabaseProcessingService : IDatabaseProcessingService
    {
        private readonly IDatabaseService databaseService;
        private readonly ILoggingBroker loggingBroker;

        public DatabaseProcessingService(
            IDatabaseService databaseService,
            ILoggingBroker loggingBroker)
        {
            this.databaseService = databaseService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Database>> RetrieveAllDatabasesAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.databaseService.RetrieveAllDatabasesAsync(cancellationToken);
                });

        public ValueTask<Database> RetrieveDatabaseByUuidAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    return await this.databaseService.RetrieveDatabaseByUuidAsync(databaseUuid, cancellationToken);
                });

        public ValueTask<PostgreSqlDatabase> AddPostgreSqlDatabaseAsync(
            PostgreSqlDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddPostgreSqlDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<MySqlDatabase> AddMySqlDatabaseAsync(
            MySqlDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddMySqlDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<MariaDbDatabase> AddMariaDbDatabaseAsync(
            MariaDbDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddMariaDbDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<MongoDbDatabase> AddMongoDbDatabaseAsync(
            MongoDbDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddMongoDbDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<RedisDatabase> AddRedisDatabaseAsync(
            RedisDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddRedisDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<ClickHouseDatabase> AddClickHouseDatabaseAsync(
            ClickHouseDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddClickHouseDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<DragonflyDatabase> AddDragonflyDatabaseAsync(
            DragonflyDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddDragonflyDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<KeyDbDatabase> AddKeyDbDatabaseAsync(
            KeyDbDatabase database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.AddKeyDbDatabaseAsync(database, cancellationToken);
                });

        public ValueTask<Database> ModifyDatabaseAsync(
            Database database, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseIsNotNull(database);

                    return await this.databaseService.ModifyDatabaseAsync(database, cancellationToken);
                });

        public ValueTask RemoveDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    await this.databaseService.RemoveDatabaseAsync(databaseUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<DatabaseBackup>> RetrieveAllBackupsAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    return await this.databaseService.RetrieveAllBackupsAsync(databaseUuid, cancellationToken);
                });

        public ValueTask<DatabaseBackup> AddBackupAsync(
            string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupIsNotNull(backup);

                    return await this.databaseService.AddBackupAsync(databaseUuid, backup, cancellationToken);
                });

        public ValueTask<DatabaseBackup> ModifyBackupAsync(
            string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupIsNotNull(backup);

                    return await this.databaseService.ModifyBackupAsync(databaseUuid, backup, cancellationToken);
                });

        public ValueTask RemoveBackupAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupUuid(backupUuid);

                    await this.databaseService.RemoveBackupAsync(databaseUuid, backupUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<BackupExecution>> RetrieveAllBackupExecutionsAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupUuid(backupUuid);

                    return await this.databaseService.RetrieveAllBackupExecutionsAsync(
                        databaseUuid, backupUuid, cancellationToken);
                });

        public ValueTask RemoveBackupExecutionAsync(
            string databaseUuid, string backupUuid, string executionUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);
                    ValidateBackupUuid(backupUuid);
                    ValidateExecutionUuid(executionUuid);

                    await this.databaseService.RemoveBackupExecutionAsync(
                        databaseUuid, backupUuid, executionUuid, cancellationToken);
                });

        public ValueTask StartDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    await this.databaseService.StartDatabaseAsync(databaseUuid, cancellationToken);
                });

        public ValueTask StopDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    await this.databaseService.StopDatabaseAsync(databaseUuid, cancellationToken);
                });

        public ValueTask RestartDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateDatabaseUuid(databaseUuid);

                    await this.databaseService.RestartDatabaseAsync(databaseUuid, cancellationToken);
                });
    }
}
