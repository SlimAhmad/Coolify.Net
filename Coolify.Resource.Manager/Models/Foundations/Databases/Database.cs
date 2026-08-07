// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.Databases
{
    public class Database
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ServerUuid { get; set; }
        public string ProjectUuid { get; set; }
        public string EnvironmentUuid { get; set; }
        public string EnvironmentName { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public int PublicPort { get; set; }
        public bool IsPublic { get; set; }
        public string DatabaseType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
