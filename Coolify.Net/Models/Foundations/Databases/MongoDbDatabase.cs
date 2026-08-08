// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Coolify.Net.Models.Foundations.Databases
{
    public class MongoDbDatabase : Database
    {
        public string MongoInitdbRootUsername { get; set; }
        public string MongoInitdbRootPassword { get; set; }
        public string MongoInitdbDatabase { get; set; }
        public string MongoConf { get; set; }
    }
}
