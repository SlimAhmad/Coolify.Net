// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class DatabaseServiceException : Xeption
    {
        public DatabaseServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
