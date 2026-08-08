// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.CoolifyServices
{
    public class CoolifyService
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DockerComposeRaw { get; set; }
        public string ServiceType { get; set; }
        public string ServerUuid { get; set; }
        public string ProjectUuid { get; set; }
        public string EnvironmentUuid { get; set; }
        public string EnvironmentName { get; set; }
        public bool InstantDeploy { get; set; }
        public string Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
