// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace Coolify.Resource.Manager.Models.Clients.Servers.Exceptions
{
    public class ServerClientValidationException : Xeption
    {
        public ServerClientValidationException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
