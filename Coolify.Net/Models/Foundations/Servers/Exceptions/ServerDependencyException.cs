// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class ServerDependencyException : Xeption
    {
        public ServerDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
