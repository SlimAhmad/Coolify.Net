// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.CoolifyServices
{
    public class ExternalCoolifyService
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("docker_compose_raw")]
        public string DockerComposeRaw { get; set; }

        [JsonPropertyName("type")]
        public string ServiceType { get; set; }

        [JsonPropertyName("server_uuid")]
        public string ServerUuid { get; set; }

        [JsonPropertyName("project_uuid")]
        public string ProjectUuid { get; set; }

        [JsonPropertyName("environment_uuid")]
        public string EnvironmentUuid { get; set; }

        [JsonPropertyName("environment_name")]
        public string EnvironmentName { get; set; }

        [JsonPropertyName("instant_deploy")]
        public bool InstantDeploy { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
