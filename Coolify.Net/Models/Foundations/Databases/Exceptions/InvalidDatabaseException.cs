// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class InvalidDatabaseException : Xeption
    {
        public InvalidDatabaseException(string message)
            : base(message)
        { }

        public InvalidDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
