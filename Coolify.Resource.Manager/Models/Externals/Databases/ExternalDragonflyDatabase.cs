// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Databases
{
    public class ExternalDragonflyDatabase : ExternalDatabase
    {
        [JsonPropertyName("dragonfly_password")]
        public string DragonflyPassword { get; set; }
    }
}
