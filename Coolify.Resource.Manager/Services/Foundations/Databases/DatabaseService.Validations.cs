// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions;

namespace Coolify.Resource.Manager.Services.Foundations.Databases
{
    public partial class DatabaseService
    {
        private static void ValidateDatabaseIsNotNull(object database)
        {
            if (database is null)
            {
                throw new NullDatabaseException(message: "Database is null.");
            }
        }

        private void ValidateDatabaseUuid(string databaseUuid) =>
            Validate((IsInvalid(databaseUuid), nameof(databaseUuid)));

        private static void ValidateBackupIsNotNull(DatabaseBackup backup)
        {
            if (backup is null)
            {
                throw new NullDatabaseException(message: "Backup is null.");
            }
        }

        private void ValidateBackupUuid(string backupUuid) =>
            Validate((IsInvalid(backupUuid), nameof(backupUuid)));

        private void ValidateExecutionUuid(string executionUuid) =>
            Validate((IsInvalid(executionUuid), nameof(executionUuid)));

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidDatabaseException =
                new InvalidDatabaseException(
                    message: "Invalid database. Please fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidDatabaseException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidDatabaseException.ThrowIfContainsErrors();
        }
    }
}
