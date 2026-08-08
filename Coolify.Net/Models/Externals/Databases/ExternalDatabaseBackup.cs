// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Databases
{
    public class ExternalDatabaseBackup
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("database_uuid")]
        public string DatabaseUuid { get; set; }

        [JsonPropertyName("frequency")]
        public string FrequencyExpression { get; set; }

        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("number_of_backups_locally")]
        public int NumberOfBackupsLocally { get; set; }

        [JsonPropertyName("s3_storage_uuid")]
        public string S3StorageUuid { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
