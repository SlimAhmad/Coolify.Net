// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Deployments.Exceptions
{
    public class TimeoutDeploymentException : Xeption
    {
        public TimeoutDeploymentException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
