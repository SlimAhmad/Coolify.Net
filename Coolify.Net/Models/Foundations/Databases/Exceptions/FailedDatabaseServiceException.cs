// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Databases.Exceptions
{
    public class FailedDatabaseServiceException : Xeption
    {
        public FailedDatabaseServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
