// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Servers
{
    public class ExternalServer
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("private_key_uuid")]
        public string PrivateKeyUuid { get; set; }

        [JsonPropertyName("proxy_enabled")]
        public bool ProxyEnabled { get; set; }

        [JsonPropertyName("proxy_type")]
        public string ProxyType { get; set; }

        [JsonPropertyName("is_reachable")]
        public bool IsReachable { get; set; }

        [JsonPropertyName("is_usable")]
        public bool IsUsable { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("settings")]
        public ExternalServerSetting Settings { get; set; }
    }
}
