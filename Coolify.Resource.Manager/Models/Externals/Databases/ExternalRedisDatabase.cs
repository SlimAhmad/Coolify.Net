// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Databases
{
    public class ExternalRedisDatabase : ExternalDatabase
    {
        [JsonPropertyName("redis_password")]
        public string RedisPassword { get; set; }

        [JsonPropertyName("redis_conf")]
        public string RedisConf { get; set; }
    }
}
