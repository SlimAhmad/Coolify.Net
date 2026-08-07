// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.Databases
{
    public class MariaDbDatabase : Database
    {
        public string MariadbRootPassword { get; set; }
        public string MariadbUser { get; set; }
        public string MariadbPassword { get; set; }
        public string MariadbDatabase { get; set; }
        public string MariadbConf { get; set; }
    }
}
