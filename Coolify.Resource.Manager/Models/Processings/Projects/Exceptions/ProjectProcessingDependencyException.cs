// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Projects.Exceptions
{
    public class ProjectProcessingDependencyException : Xeption
    {
        public ProjectProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
