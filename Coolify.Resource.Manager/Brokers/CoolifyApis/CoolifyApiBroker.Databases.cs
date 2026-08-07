// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Databases;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string DatabasesRelativeUrl = "databases";

        public async ValueTask<IEnumerable<ExternalDatabase>> GetAllDatabasesAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalDatabase>>(DatabasesRelativeUrl, cancellationToken);

        public async ValueTask<ExternalDatabase> GetDatabaseByUuidAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalDatabase>($"{DatabasesRelativeUrl}/{databaseUuid}", cancellationToken);

        public async ValueTask<ExternalPostgreSqlDatabase> PostPostgreSqlDatabaseAsync(
            ExternalPostgreSqlDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalPostgreSqlDatabase>(
                    $"{DatabasesRelativeUrl}/postgresql", database, cancellationToken);

        public async ValueTask<ExternalMySqlDatabase> PostMySqlDatabaseAsync(
            ExternalMySqlDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalMySqlDatabase>(
                    $"{DatabasesRelativeUrl}/mysql", database, cancellationToken);

        public async ValueTask<ExternalMariaDbDatabase> PostMariaDbDatabaseAsync(
            ExternalMariaDbDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalMariaDbDatabase>(
                    $"{DatabasesRelativeUrl}/mariadb", database, cancellationToken);

        public async ValueTask<ExternalMongoDbDatabase> PostMongoDbDatabaseAsync(
            ExternalMongoDbDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalMongoDbDatabase>(
                    $"{DatabasesRelativeUrl}/mongodb", database, cancellationToken);

        public async ValueTask<ExternalRedisDatabase> PostRedisDatabaseAsync(
            ExternalRedisDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalRedisDatabase>(
                    $"{DatabasesRelativeUrl}/redis", database, cancellationToken);

        public async ValueTask<ExternalClickHouseDatabase> PostClickHouseDatabaseAsync(
            ExternalClickHouseDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalClickHouseDatabase>(
                    $"{DatabasesRelativeUrl}/clickhouse", database, cancellationToken);

        public async ValueTask<ExternalDragonflyDatabase> PostDragonflyDatabaseAsync(
            ExternalDragonflyDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalDragonflyDatabase>(
                    $"{DatabasesRelativeUrl}/dragonfly", database, cancellationToken);

        public async ValueTask<ExternalKeyDbDatabase> PostKeyDbDatabaseAsync(
            ExternalKeyDbDatabase database, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalKeyDbDatabase>(
                    $"{DatabasesRelativeUrl}/keydb", database, cancellationToken);

        public async ValueTask<ExternalDatabase> PatchDatabaseAsync(
            ExternalDatabase database, CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalDatabase>(
                    $"{DatabasesRelativeUrl}/{database.Uuid}", database, cancellationToken);

        public async ValueTask DeleteDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync($"{DatabasesRelativeUrl}/{databaseUuid}", cancellationToken);

        public async ValueTask<IEnumerable<ExternalDatabaseBackup>> GetDatabaseBackupsAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalDatabaseBackup>>(
                    $"{DatabasesRelativeUrl}/{databaseUuid}/backups", cancellationToken);

        public async ValueTask<ExternalDatabaseBackup> PostDatabaseBackupAsync(
            string databaseUuid, ExternalDatabaseBackup backup, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalDatabaseBackup>(
                    $"{DatabasesRelativeUrl}/{databaseUuid}/backups", backup, cancellationToken);

        public async ValueTask<ExternalDatabaseBackup> PatchDatabaseBackupAsync(
            string databaseUuid,
            string backupUuid,
            ExternalDatabaseBackup backup,
            CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalDatabaseBackup>(
                    $"{DatabasesRelativeUrl}/{databaseUuid}/backups/{backupUuid}", backup, cancellationToken);

        public async ValueTask DeleteDatabaseBackupAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync(
                    $"{DatabasesRelativeUrl}/{databaseUuid}/backups/{backupUuid}", cancellationToken);

        public async ValueTask<IEnumerable<ExternalBackupExecution>> GetDatabaseBackupExecutionsAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalBackupExecution>>(
                    $"{DatabasesRelativeUrl}/{databaseUuid}/backups/{backupUuid}/executions", cancellationToken);

        public async ValueTask DeleteDatabaseBackupExecutionAsync(
            string databaseUuid,
            string backupUuid,
            string executionUuid,
            CancellationToken cancellationToken = default) =>
                await DeleteAsync(
                    $"{DatabasesRelativeUrl}/{databaseUuid}/backups/{backupUuid}/executions/{executionUuid}",
                    cancellationToken);

        public async ValueTask PostDatabaseStartAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{DatabasesRelativeUrl}/{databaseUuid}/start", cancellationToken);

        public async ValueTask PostDatabaseStopAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{DatabasesRelativeUrl}/{databaseUuid}/stop", cancellationToken);

        public async ValueTask PostDatabaseRestartAsync(
            string databaseUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{DatabasesRelativeUrl}/{databaseUuid}/restart", cancellationToken);
    }
}
