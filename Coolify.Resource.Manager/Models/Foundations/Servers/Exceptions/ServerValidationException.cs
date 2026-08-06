// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions
{
    public class ServerValidationException : Xeption
    {
        public ServerValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
