// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace Coolify.Resource.Manager.Models.Clients.Systems.Exceptions
{
    public class SystemClientDependencyException : Xeption
    {
        public SystemClientDependencyException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
