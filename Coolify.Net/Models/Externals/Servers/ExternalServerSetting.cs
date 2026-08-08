// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Servers
{
    public class ExternalServerSetting
    {
        [JsonPropertyName("is_build_server")]
        public bool IsBuildServer { get; set; }

        [JsonPropertyName("is_swarm_manager")]
        public bool IsSwarmManager { get; set; }

        [JsonPropertyName("is_swarm_worker")]
        public bool IsSwarmWorker { get; set; }

        [JsonPropertyName("sentinel_enabled")]
        public bool SentinelEnabled { get; set; }

        [JsonPropertyName("sentinel_token")]
        public string SentinelToken { get; set; }

        [JsonPropertyName("is_reachable")]
        public bool IsReachable { get; set; }

        [JsonPropertyName("is_usable")]
        public bool IsUsable { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
