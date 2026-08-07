// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Databases.Exceptions
{
    public class DatabaseProcessingDependencyValidationException : Xeption
    {
        public DatabaseProcessingDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
