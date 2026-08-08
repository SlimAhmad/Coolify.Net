// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Databases.Exceptions
{
    public class InvalidDatabaseProcessingException : Xeption
    {
        public InvalidDatabaseProcessingException(string message)
            : base(message)
        { }
    }
}
