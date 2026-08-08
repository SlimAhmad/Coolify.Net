// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.CoolifyServices
{
    public class ServiceApplication
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string ServiceUuid { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public bool IsPublic { get; set; }
        public string Fqdn { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
