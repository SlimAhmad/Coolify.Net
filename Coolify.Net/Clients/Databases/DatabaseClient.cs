// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Databases.Exceptions;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Processings.Databases.Exceptions;
using Coolify.Net.Services.Processings.Databases;
using Xeptions;

namespace Coolify.Net.Clients.Databases
{
    /// <summary>Provides database provisioning and management operations.</summary>
    internal class DatabaseClient : IDatabaseClient
    {
        private readonly IDatabaseProcessingService databaseProcessingService;

        public DatabaseClient(IDatabaseProcessingService databaseProcessingService) =>
            this.databaseProcessingService = databaseProcessingService;

        public async ValueTask<IEnumerable<Database>> RetrieveAllDatabasesAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.RetrieveAllDatabasesAsync(cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Database> RetrieveDatabaseByUuidAsync(
            string databaseUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.RetrieveDatabaseByUuidAsync(databaseUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<PostgreSqlDatabase> AddPostgreSqlDatabaseAsync(
            PostgreSqlDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddPostgreSqlDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<MySqlDatabase> AddMySqlDatabaseAsync(
            MySqlDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddMySqlDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<MariaDbDatabase> AddMariaDbDatabaseAsync(
            MariaDbDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddMariaDbDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<MongoDbDatabase> AddMongoDbDatabaseAsync(
            MongoDbDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddMongoDbDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<RedisDatabase> AddRedisDatabaseAsync(
            RedisDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddRedisDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<ClickHouseDatabase> AddClickHouseDatabaseAsync(
            ClickHouseDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddClickHouseDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<DragonflyDatabase> AddDragonflyDatabaseAsync(
            DragonflyDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddDragonflyDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<KeyDbDatabase> AddKeyDbDatabaseAsync(
            KeyDbDatabase database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddKeyDbDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Database> ModifyDatabaseAsync(
            Database database, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.ModifyDatabaseAsync(database, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.databaseProcessingService.RemoveDatabaseAsync(databaseUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<DatabaseBackup>> RetrieveAllBackupsAsync(
            string databaseUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.RetrieveAllBackupsAsync(databaseUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<DatabaseBackup> AddBackupAsync(
            string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.AddBackupAsync(databaseUuid, backup, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<DatabaseBackup> ModifyBackupAsync(
            string databaseUuid, DatabaseBackup backup, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.ModifyBackupAsync(databaseUuid, backup, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveBackupAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.databaseProcessingService.RemoveBackupAsync(databaseUuid, backupUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<BackupExecution>> RetrieveAllBackupExecutionsAsync(
            string databaseUuid, string backupUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.databaseProcessingService.RetrieveAllBackupExecutionsAsync(
                    databaseUuid, backupUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveBackupExecutionAsync(
            string databaseUuid, string backupUuid, string executionUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.databaseProcessingService.RemoveBackupExecutionAsync(
                    databaseUuid, backupUuid, executionUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask StartDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.databaseProcessingService.StartDatabaseAsync(databaseUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask StopDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.databaseProcessingService.StopDatabaseAsync(databaseUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RestartDatabaseAsync(
            string databaseUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.databaseProcessingService.RestartDatabaseAsync(databaseUuid, cancellationToken);
            }
            catch (DatabaseProcessingValidationException databaseValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyValidationException databaseDependencyValidationException)
            {
                throw CreateDatabaseClientValidationException(databaseDependencyValidationException.InnerException as Xeption);
            }
            catch (DatabaseProcessingDependencyException databaseDependencyException)
            {
                throw CreateDatabaseClientDependencyException(databaseDependencyException.InnerException as Xeption);
            }
            catch (DatabaseProcessingServiceException databaseServiceException)
            {
                throw CreateDatabaseClientDependencyException(databaseServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateDatabaseClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static DatabaseClientValidationException
            CreateDatabaseClientValidationException(Xeption innerException)
        {
            return new DatabaseClientValidationException(
                message: "Database client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static DatabaseClientDependencyException
            CreateDatabaseClientDependencyException(Xeption innerException)
        {
            return new DatabaseClientDependencyException(
                message: "Database client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static DatabaseClientServiceException
            CreateDatabaseClientServiceException(Xeption innerException)
        {
            return new DatabaseClientServiceException(
                message: "Database client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
