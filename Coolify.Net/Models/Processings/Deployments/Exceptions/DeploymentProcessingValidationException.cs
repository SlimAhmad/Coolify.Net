// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Deployments.Exceptions
{
    public class DeploymentProcessingValidationException : Xeption
    {
        public DeploymentProcessingValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
