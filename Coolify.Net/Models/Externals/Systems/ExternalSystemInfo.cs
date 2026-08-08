// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.Systems
{
    public class ExternalSystemInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
