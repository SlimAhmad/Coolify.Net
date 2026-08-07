// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Deployments.Exceptions
{
    public class DeploymentProcessingServiceException : Xeption
    {
        public DeploymentProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
