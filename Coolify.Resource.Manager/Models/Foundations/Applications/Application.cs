// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.Applications
{
    public class Application
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Fqdn { get; set; }
        public string GitRepository { get; set; }
        public string GitBranch { get; set; }
        public string GitCommitSha { get; set; }
        public string BuildPack { get; set; }
        public string DockerfileLocation { get; set; }
        public string DockerComposeLocation { get; set; }
        public string DockerComposeRaw { get; set; }
        public string DockerImage { get; set; }
        public string ServerUuid { get; set; }
        public string ProjectUuid { get; set; }
        public string EnvironmentUuid { get; set; }
        public string EnvironmentName { get; set; }
        public string Status { get; set; }
        public bool InstantDeploy { get; set; }
        public bool AutoDeployEnabled { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
