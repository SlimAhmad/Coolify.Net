// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;

namespace Coolify.Resource.Manager.Clients.Databases
{
    /// <summary>Defines the contract for managing Coolify databases.</summary>
    public interface IDatabaseClient
    {
        /// <summary>Retrieves all databases accessible by the configured team.</summary>
        /// <exception cref="Exceptions.DatabaseClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.DatabaseClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.DatabaseClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<Database>> RetrieveAllDatabasesAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a database by its UUID.</summary>
        ValueTask<Database> RetrieveDatabaseByUuidAsync(string databaseUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new PostgreSQL database.</summary>
        ValueTask<PostgreSqlDatabase> AddPostgreSqlDatabaseAsync(PostgreSqlDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new MySQL database.</summary>
        ValueTask<MySqlDatabase> AddMySqlDatabaseAsync(MySqlDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new MariaDB database.</summary>
        ValueTask<MariaDbDatabase> AddMariaDbDatabaseAsync(MariaDbDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new MongoDB database.</summary>
        ValueTask<MongoDbDatabase> AddMongoDbDatabaseAsync(MongoDbDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new Redis database.</summary>
        ValueTask<RedisDatabase> AddRedisDatabaseAsync(RedisDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new ClickHouse database.</summary>
        ValueTask<ClickHouseDatabase> AddClickHouseDatabaseAsync(ClickHouseDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new Dragonfly database.</summary>
        ValueTask<DragonflyDatabase> AddDragonflyDatabaseAsync(DragonflyDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Creates a new KeyDB database.</summary>
        ValueTask<KeyDbDatabase> AddKeyDbDatabaseAsync(KeyDbDatabase database, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing database.</summary>
        ValueTask<Database> ModifyDatabaseAsync(Database database, CancellationToken cancellationToken = default);

        /// <summary>Deletes a database.</summary>
        ValueTask RemoveDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists all backup configurations for a database.</summary>
        ValueTask<IEnumerable<DatabaseBackup>> RetrieveAllBackupsAsync(string databaseUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new backup schedule for a database.</summary>
        ValueTask<DatabaseBackup> AddBackupAsync(string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing backup schedule.</summary>
        ValueTask<DatabaseBackup> ModifyBackupAsync(string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default);

        /// <summary>Deletes a backup schedule.</summary>
        ValueTask RemoveBackupAsync(string databaseUuid, string backupUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists backup execution history for a backup schedule.</summary>
        ValueTask<IEnumerable<BackupExecution>> RetrieveAllBackupExecutionsAsync(string databaseUuid, string backupUuid, CancellationToken cancellationToken = default);

        /// <summary>Deletes a backup execution record.</summary>
        ValueTask RemoveBackupExecutionAsync(string databaseUuid, string backupUuid, string executionUuid, CancellationToken cancellationToken = default);

        /// <summary>Starts the database.</summary>
        ValueTask StartDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);

        /// <summary>Stops the database.</summary>
        ValueTask StopDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);

        /// <summary>Restarts the database.</summary>
        ValueTask RestartDatabaseAsync(string databaseUuid, CancellationToken cancellationToken = default);
    }
}
