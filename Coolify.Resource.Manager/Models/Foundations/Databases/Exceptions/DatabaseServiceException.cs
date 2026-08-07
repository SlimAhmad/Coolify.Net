// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions
{
    public class DatabaseServiceException : Xeption
    {
        public DatabaseServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
