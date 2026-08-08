// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Deployments.Exceptions
{
    public class DeploymentProcessingDependencyException : Xeption
    {
        public DeploymentProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
