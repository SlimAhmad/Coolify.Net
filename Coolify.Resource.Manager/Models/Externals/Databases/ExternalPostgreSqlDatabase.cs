// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Databases
{
    public class ExternalPostgreSqlDatabase : ExternalDatabase
    {
        [JsonPropertyName("postgres_user")]
        public string PostgresUser { get; set; }

        [JsonPropertyName("postgres_password")]
        public string PostgresPassword { get; set; }

        [JsonPropertyName("postgres_db")]
        public string PostgresDb { get; set; }

        [JsonPropertyName("postgres_initdb_args")]
        public string PostgresInitdbArgs { get; set; }

        [JsonPropertyName("postgres_host_auth_method")]
        public string PostgresHostAuthMethod { get; set; }

        [JsonPropertyName("postgres_conf")]
        public string PostgresConf { get; set; }
    }
}
