// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class ServerDependencyValidationException : Xeption
    {
        public ServerDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
