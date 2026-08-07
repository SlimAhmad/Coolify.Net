// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.Databases
{
    public class PostgreSqlDatabase : Database
    {
        public string PostgresUser { get; set; }
        public string PostgresPassword { get; set; }
        public string PostgresDb { get; set; }
        public string PostgresInitdbArgs { get; set; }
        public string PostgresHostAuthMethod { get; set; }
        public string PostgresConf { get; set; }
    }
}
