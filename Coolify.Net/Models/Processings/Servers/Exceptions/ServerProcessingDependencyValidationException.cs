// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.Servers.Exceptions
{
    public class ServerProcessingDependencyValidationException : Xeption
    {
        public ServerProcessingDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
