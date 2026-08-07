// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.PrivateKeys
{
    public class PrivateKey
    {
        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrivateKeyValue { get; set; }
        public string TeamId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
