// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions
{
    public class FailedProjectDependencyException : Xeption
    {
        public FailedProjectDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
