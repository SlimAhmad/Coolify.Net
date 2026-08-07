// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Databases.Exceptions
{
    public class InvalidDatabaseProcessingException : Xeption
    {
        public InvalidDatabaseProcessingException(string message)
            : base(message)
        { }
    }
}
