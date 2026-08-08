// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class TimeoutDatabaseException : Xeption
    {
        public TimeoutDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
