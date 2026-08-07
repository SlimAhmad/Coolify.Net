// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Databases;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalDatabase>> GetAllDatabasesAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalDatabase> GetDatabaseByUuidAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalPostgreSqlDatabase> PostPostgreSqlDatabaseAsync(ExternalPostgreSqlDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalMySqlDatabase> PostMySqlDatabaseAsync(ExternalMySqlDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalMariaDbDatabase> PostMariaDbDatabaseAsync(ExternalMariaDbDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalMongoDbDatabase> PostMongoDbDatabaseAsync(ExternalMongoDbDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalRedisDatabase> PostRedisDatabaseAsync(ExternalRedisDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalClickHouseDatabase> PostClickHouseDatabaseAsync(ExternalClickHouseDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalDragonflyDatabase> PostDragonflyDatabaseAsync(ExternalDragonflyDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalKeyDbDatabase> PostKeyDbDatabaseAsync(ExternalKeyDbDatabase database, CancellationToken cancellationToken = default);
        ValueTask<ExternalDatabase> PatchDatabaseAsync(ExternalDatabase database, CancellationToken cancellationToken = default);
        ValueTask DeleteDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalDatabaseBackup>> GetDatabaseBackupsAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalDatabaseBackup> PostDatabaseBackupAsync(string databaseUuid, ExternalDatabaseBackup backup, CancellationToken cancellationToken = default);
        ValueTask<ExternalDatabaseBackup> PatchDatabaseBackupAsync(string databaseUuid, string backupUuid, ExternalDatabaseBackup backup, CancellationToken cancellationToken = default);
        ValueTask DeleteDatabaseBackupAsync(string databaseUuid, string backupUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalBackupExecution>> GetDatabaseBackupExecutionsAsync(string databaseUuid, string backupUuid, CancellationToken cancellationToken = default);
        ValueTask DeleteDatabaseBackupExecutionAsync(string databaseUuid, string backupUuid, string executionUuid, CancellationToken cancellationToken = default);
        ValueTask PostDatabaseStartAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask PostDatabaseStopAsync(string databaseUuid, CancellationToken cancellationToken = default);
        ValueTask PostDatabaseRestartAsync(string databaseUuid, CancellationToken cancellationToken = default);
    }
}
