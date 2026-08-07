// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions
{
    public class InvalidDeploymentException : Xeption
    {
        public InvalidDeploymentException(string message)
            : base(message)
        { }

        public InvalidDeploymentException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
