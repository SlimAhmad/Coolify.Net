// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions
{
    public class TimeoutDatabaseException : Xeption
    {
        public TimeoutDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
