// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables
{
    public class EnvironmentVariable
    {
        public string Uuid { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool IsPreview { get; set; }
        public bool IsBuildTime { get; set; }
        public bool IsLiteral { get; set; }
        public bool IsMultiline { get; set; }
        public bool IsShownOnce { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
