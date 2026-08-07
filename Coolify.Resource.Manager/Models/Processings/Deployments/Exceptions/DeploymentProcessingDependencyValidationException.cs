// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Deployments.Exceptions
{
    public class DeploymentProcessingDependencyValidationException : Xeption
    {
        public DeploymentProcessingDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
