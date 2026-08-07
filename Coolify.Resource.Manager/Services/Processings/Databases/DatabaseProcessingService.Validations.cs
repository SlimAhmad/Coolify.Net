// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Models.Processings.Databases.Exceptions;

namespace Coolify.Resource.Manager.Services.Processings.Databases
{
    public partial class DatabaseProcessingService
    {
        private static void ValidateDatabaseIsNotNull(object database)
        {
            if (database is null)
            {
                throw new NullDatabaseProcessingException(message: "Database is null.");
            }
        }

        private static void ValidateDatabaseUuid(string databaseUuid)
        {
            if (string.IsNullOrWhiteSpace(databaseUuid))
            {
                throw new InvalidDatabaseProcessingException(message: "Database uuid is invalid.");
            }
        }

        private static void ValidateBackupIsNotNull(DatabaseBackup backup)
        {
            if (backup is null)
            {
                throw new NullDatabaseProcessingException(message: "Backup is null.");
            }
        }

        private static void ValidateBackupUuid(string backupUuid)
        {
            if (string.IsNullOrWhiteSpace(backupUuid))
            {
                throw new InvalidDatabaseProcessingException(message: "Backup uuid is invalid.");
            }
        }

        private static void ValidateExecutionUuid(string executionUuid)
        {
            if (string.IsNullOrWhiteSpace(executionUuid))
            {
                throw new InvalidDatabaseProcessingException(message: "Execution uuid is invalid.");
            }
        }
    }
}
