// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net
{
    public class CoolifyClientOptions
    {
        public string BaseUrl { get; set; }
        public string ApiToken { get; set; }
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);
    }
}
