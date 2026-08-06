// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.Servers
{
    public class ServerSetting
    {
        public bool IsBuildServer { get; set; }
        public bool IsSwarmManager { get; set; }
        public bool IsSwarmWorker { get; set; }
        public bool SentinelEnabled { get; set; }
        public string SentinelToken { get; set; }
        public bool IsReachable { get; set; }
        public bool IsUsable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
