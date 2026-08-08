// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Databases
{
    public class ExternalMongoDbDatabase : ExternalDatabase
    {
        [JsonPropertyName("mongo_initdb_root_username")]
        public string MongoInitdbRootUsername { get; set; }

        [JsonPropertyName("mongo_initdb_root_password")]
        public string MongoInitdbRootPassword { get; set; }

        [JsonPropertyName("mongo_initdb_database")]
        public string MongoInitdbDatabase { get; set; }

        [JsonPropertyName("mongo_conf")]
        public string MongoConf { get; set; }
    }
}
