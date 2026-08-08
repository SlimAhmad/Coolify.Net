// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Foundations.Servers.Exceptions
{
    public class ServerValidationException : Xeption
    {
        public ServerValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
