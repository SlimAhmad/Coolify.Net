// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Deployments.Exceptions
{
    public class NullDeploymentProcessingException : Xeption
    {
        public NullDeploymentProcessingException(string message)
            : base(message)
        { }
    }
}
