// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Applications.Exceptions
{
    public class ApplicationDependencyException : Xeption
    {
        public ApplicationDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
