// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.Servers.Exceptions
{
    public class ServerProcessingDependencyException : Xeption
    {
        public ServerProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
