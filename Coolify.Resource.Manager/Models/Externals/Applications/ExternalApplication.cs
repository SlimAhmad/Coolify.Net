// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Applications
{
    public class ExternalApplication
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("fqdn")]
        public string Fqdn { get; set; }

        [JsonPropertyName("git_repository")]
        public string GitRepository { get; set; }

        [JsonPropertyName("git_branch")]
        public string GitBranch { get; set; }

        [JsonPropertyName("git_commit_sha")]
        public string GitCommitSha { get; set; }

        [JsonPropertyName("build_pack")]
        public string BuildPack { get; set; }

        [JsonPropertyName("dockerfile_location")]
        public string DockerfileLocation { get; set; }

        [JsonPropertyName("docker_compose_location")]
        public string DockerComposeLocation { get; set; }

        [JsonPropertyName("docker_compose_raw")]
        public string DockerComposeRaw { get; set; }

        [JsonPropertyName("docker_image")]
        public string DockerImage { get; set; }

        [JsonPropertyName("server_uuid")]
        public string ServerUuid { get; set; }

        [JsonPropertyName("project_uuid")]
        public string ProjectUuid { get; set; }

        [JsonPropertyName("environment_uuid")]
        public string EnvironmentUuid { get; set; }

        [JsonPropertyName("environment_name")]
        public string EnvironmentName { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("instant_deploy")]
        public bool InstantDeploy { get; set; }

        [JsonPropertyName("auto_deploy_enabled")]
        public bool AutoDeployEnabled { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
