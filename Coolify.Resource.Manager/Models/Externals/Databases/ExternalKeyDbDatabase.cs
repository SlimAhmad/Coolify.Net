// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Databases
{
    public class ExternalKeyDbDatabase : ExternalDatabase
    {
        [JsonPropertyName("keydb_password")]
        public string KeydbPassword { get; set; }

        [JsonPropertyName("keydb_conf")]
        public string KeydbConf { get; set; }
    }
}
