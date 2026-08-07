// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions
{
    public class FailedDeploymentDependencyException : Xeption
    {
        public FailedDeploymentDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
