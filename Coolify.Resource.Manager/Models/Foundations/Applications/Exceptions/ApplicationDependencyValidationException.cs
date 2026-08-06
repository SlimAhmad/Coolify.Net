// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions
{
    public class ApplicationDependencyValidationException : Xeption
    {
        public ApplicationDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
