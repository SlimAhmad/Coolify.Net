// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.Servers
{
    public class Server
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ip { get; set; }
        public string User { get; set; }
        public int Port { get; set; }
        public string PrivateKeyUuid { get; set; }
        public bool ProxyEnabled { get; set; }
        public string ProxyType { get; set; }
        public bool IsReachable { get; set; }
        public bool IsUsable { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public ServerSetting Settings { get; set; }
    }
}
