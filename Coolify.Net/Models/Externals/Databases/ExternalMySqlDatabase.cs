// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Databases
{
    public class ExternalMySqlDatabase : ExternalDatabase
    {
        [JsonPropertyName("mysql_root_password")]
        public string MysqlRootPassword { get; set; }

        [JsonPropertyName("mysql_user")]
        public string MysqlUser { get; set; }

        [JsonPropertyName("mysql_password")]
        public string MysqlPassword { get; set; }

        [JsonPropertyName("mysql_database")]
        public string MysqlDatabase { get; set; }

        [JsonPropertyName("mysql_conf")]
        public string MysqlConf { get; set; }
    }
}
