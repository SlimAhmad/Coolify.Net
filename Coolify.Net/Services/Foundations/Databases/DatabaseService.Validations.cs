// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;

namespace Coolify.Net.Services.Foundations.Databases
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
            Validate(
                message: "Invalid database. Please fix the errors and try again.",
                (Rule: IsInvalid(databaseUuid), Parameter: nameof(databaseUuid)));

        private static void ValidateBackupIsNotNull(DatabaseBackup backup)
        {
            if (backup is null)
            {
                throw new NullDatabaseException(message: "Backup is null.");
            }
        }

        private void ValidateBackupUuid(string backupUuid) =>
            Validate(
                message: "Invalid database. Please fix the errors and try again.",
                (Rule: IsInvalid(backupUuid), Parameter: nameof(backupUuid)));

        private void ValidateExecutionUuid(string executionUuid) =>
            Validate(
                message: "Invalid database. Please fix the errors and try again.",
                (Rule: IsInvalid(executionUuid), Parameter: nameof(executionUuid)));

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(string message, params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidDatabaseException = new InvalidDatabaseException(message);

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
