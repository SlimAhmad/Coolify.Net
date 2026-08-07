// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Resource.Manager.Models.Externals.Systems
{
    public class ExternalSystemInfo
    {
        [JsonPropertyName("version")]
        public string Version { get; set; }
    }
}
