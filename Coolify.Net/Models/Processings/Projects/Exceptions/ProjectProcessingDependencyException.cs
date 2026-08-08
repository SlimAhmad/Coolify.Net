// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Projects.Exceptions
{
    public class ProjectProcessingDependencyException : Xeption
    {
        public ProjectProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
