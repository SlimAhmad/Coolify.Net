// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Applications.Exceptions
{
    public class ApplicationProcessingDependencyException : Xeption
    {
        public ApplicationProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
