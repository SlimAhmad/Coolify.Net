// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.Deployments
{
    public class Deployment
    {
        public string Uuid { get; set; }
        public string ApplicationUuid { get; set; }
        public string ServerUuid { get; set; }
        public string Status { get; set; }
        public string Logs { get; set; }
        public string CommitSha { get; set; }
        public string Branch { get; set; }
        public bool ForceRebuild { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
