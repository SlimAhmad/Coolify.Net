// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Deployments.Exceptions
{
    public class FailedDeploymentServiceException : Xeption
    {
        public FailedDeploymentServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
