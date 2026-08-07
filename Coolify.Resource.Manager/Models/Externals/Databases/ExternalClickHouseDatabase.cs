// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Databases
{
    public class ExternalClickHouseDatabase : ExternalDatabase
    {
        [JsonPropertyName("clickhouse_admin_user")]
        public string ClickhouseAdminUser { get; set; }

        [JsonPropertyName("clickhouse_admin_password")]
        public string ClickhouseAdminPassword { get; set; }
    }
}
