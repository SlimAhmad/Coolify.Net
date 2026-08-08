// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Coolify.Net.Models.Externals.EnvironmentVariables
{
    public class ExternalEnvironmentVariable
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("is_preview")]
        public bool IsPreview { get; set; }

        [JsonPropertyName("is_build_time")]
        public bool IsBuildTime { get; set; }

        [JsonPropertyName("is_literal")]
        public bool IsLiteral { get; set; }

        [JsonPropertyName("is_multiline")]
        public bool IsMultiline { get; set; }

        [JsonPropertyName("is_shown_once")]
        public bool IsShownOnce { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
