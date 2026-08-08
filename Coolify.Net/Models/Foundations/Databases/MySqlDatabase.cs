// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.Databases
{
    public class MySqlDatabase : Database
    {
        public string MysqlRootPassword { get; set; }
        public string MysqlUser { get; set; }
        public string MysqlPassword { get; set; }
        public string MysqlDatabase { get; set; }
        public string MysqlConf { get; set; }
    }
}
