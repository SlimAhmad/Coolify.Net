// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Deployments.Exceptions
{
    public class DeploymentProcessingServiceException : Xeption
    {
        public DeploymentProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
