// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Provision.Models.Configurations
{
    public class CoolifyProvisionConfiguration
    {
        public CoolifyOptionsConfiguration Coolify { get; set; }
        public string ProjectName { get; set; }
        public string ServerUuid { get; set; }
        public PostgresConfiguration Postgres { get; set; }
        public GitApplicationConfiguration Website { get; set; }
        public GitApplicationConfiguration WebApi { get; set; }
        public ProvisionAction Up { get; set; }
        public ProvisionAction Down { get; set; }
    }
}
