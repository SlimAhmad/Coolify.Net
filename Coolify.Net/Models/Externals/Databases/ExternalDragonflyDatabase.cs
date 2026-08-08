// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Databases
{
    public class ExternalDragonflyDatabase : ExternalDatabase
    {
        [JsonPropertyName("dragonfly_password")]
        public string DragonflyPassword { get; set; }
    }
}
