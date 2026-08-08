// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Deployments.Exceptions
{
    public class TimeoutDeploymentProcessingException : Xeption
    {
        public TimeoutDeploymentProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
