// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.Databases
{
    public class DatabaseBackup
    {
        public string Uuid { get; set; }
        public string DatabaseUuid { get; set; }
        public string FrequencyExpression { get; set; }
        public bool Enabled { get; set; }
        public int NumberOfBackupsLocally { get; set; }
        public string S3StorageUuid { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
