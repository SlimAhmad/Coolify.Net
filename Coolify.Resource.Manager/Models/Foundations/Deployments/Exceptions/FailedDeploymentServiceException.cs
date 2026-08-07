// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions
{
    public class FailedDeploymentServiceException : Xeption
    {
        public FailedDeploymentServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
