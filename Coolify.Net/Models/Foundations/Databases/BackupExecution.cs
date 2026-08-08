// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.Databases
{
    public class BackupExecution
    {
        public string Uuid { get; set; }
        public string BackupUuid { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Filename { get; set; }
        public long Size { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
