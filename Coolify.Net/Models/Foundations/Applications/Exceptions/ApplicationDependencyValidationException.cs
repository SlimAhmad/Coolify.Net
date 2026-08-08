// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Applications.Exceptions
{
    public class ApplicationDependencyValidationException : Xeption
    {
        public ApplicationDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
