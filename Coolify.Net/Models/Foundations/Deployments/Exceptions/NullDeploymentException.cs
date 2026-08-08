// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Deployments.Exceptions
{
    public class NullDeploymentException : Xeption
    {
        public NullDeploymentException(string message)
            : base(message)
        { }
    }
}
