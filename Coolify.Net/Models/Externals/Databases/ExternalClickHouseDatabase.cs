// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Databases
{
    public class ExternalClickHouseDatabase : ExternalDatabase
    {
        [JsonPropertyName("clickhouse_admin_user")]
        public string ClickhouseAdminUser { get; set; }

        [JsonPropertyName("clickhouse_admin_password")]
        public string ClickhouseAdminPassword { get; set; }
    }
}
