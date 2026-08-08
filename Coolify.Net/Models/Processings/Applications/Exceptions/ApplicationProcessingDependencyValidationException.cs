// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Applications.Exceptions
{
    public class ApplicationProcessingDependencyValidationException : Xeption
    {
        public ApplicationProcessingDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
