// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Databases
{
    public class ExternalMariaDbDatabase : ExternalDatabase
    {
        [JsonPropertyName("mariadb_root_password")]
        public string MariadbRootPassword { get; set; }

        [JsonPropertyName("mariadb_user")]
        public string MariadbUser { get; set; }

        [JsonPropertyName("mariadb_password")]
        public string MariadbPassword { get; set; }

        [JsonPropertyName("mariadb_database")]
        public string MariadbDatabase { get; set; }

        [JsonPropertyName("mariadb_conf")]
        public string MariadbConf { get; set; }
    }
}
