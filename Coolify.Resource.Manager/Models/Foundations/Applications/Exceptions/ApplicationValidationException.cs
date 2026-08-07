// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions
{
    public class ApplicationValidationException : Xeption
    {
        public ApplicationValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
