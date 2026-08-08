// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Deployments.Exceptions
{
    public class InvalidDeploymentProcessingException : Xeption
    {
        public InvalidDeploymentProcessingException(string message)
            : base(message)
        { }
    }
}
