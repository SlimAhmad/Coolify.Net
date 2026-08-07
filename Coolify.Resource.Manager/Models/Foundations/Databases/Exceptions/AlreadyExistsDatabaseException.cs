// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions
{
    public class AlreadyExistsDatabaseException : Xeption
    {
        public AlreadyExistsDatabaseException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
