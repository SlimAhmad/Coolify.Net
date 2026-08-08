// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class FailedServerDependencyException : Xeption
    {
        public FailedServerDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
