// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;

namespace Coolify.Resource.Manager.Services.Processings.Databases
{
    public interface IDatabaseProcessingService
    {
        ValueTask<IEnumerable<Database>> RetrieveAllDatabasesAsync(CancellationToken cancellationToken = default);
        ValueTask<Database> RetrieveDatabaseByUuidAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask<PostgreSqlDatabase> AddPostgreSqlDatabaseAsync(PostgreSqlDatabase database, CancellationToken cancellationToken = default);
        ValueTask<MySqlDatabase> AddMySqlDatabaseAsync(MySqlDatabase database, CancellationToken cancellationToken = default);
        ValueTask<MariaDbDatabase> AddMariaDbDatabaseAsync(MariaDbDatabase database, CancellationToken cancellationToken = default);
        ValueTask<MongoDbDatabase> AddMongoDbDatabaseAsync(MongoDbDatabase database, CancellationToken cancellationToken = default);
        ValueTask<RedisDatabase> AddRedisDatabaseAsync(RedisDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ClickHouseDatabase> AddClickHouseDatabaseAsync(ClickHouseDatabase database, CancellationToken cancellationToken = default);
        ValueTask<DragonflyDatabase> AddDragonflyDatabaseAsync(DragonflyDatabase database, CancellationToken cancellationToken = default);
        ValueTask<KeyDbDatabase> AddKeyDbDatabaseAsync(KeyDbDatabase database, CancellationToken cancellationToken = default);
        ValueTask<Database> ModifyDatabaseAsync(Database database, CancellationToken cancellationToken = default);
        ValueTask RemoveDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<DatabaseBackup>> RetrieveAllBackupsAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask<DatabaseBackup> AddBackupAsync(string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default);
        ValueTask<DatabaseBackup> ModifyBackupAsync(string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default);
        ValueTask RemoveBackupAsync(string databaseUuid, string backupUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<BackupExecution>> RetrieveAllBackupExecutionsAsync(string databaseUuid, string backupUuid, CancellationToken cancellationToken = default);
        ValueTask RemoveBackupExecutionAsync(string databaseUuid, string backupUuid, string executionUuid, CancellationToken cancellationToken = default);
        ValueTask StartDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask StopDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask RestartDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);
    }
}
