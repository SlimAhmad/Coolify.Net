// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Collections;
using Xeptions;

namespace Coolify.Resource.Manager.Models.Clients.PrivateKeys.Exceptions
{
    public class PrivateKeyClientValidationException : Xeption
    {
        public PrivateKeyClientValidationException(string message, Xeption innerException, IDictionary data)
            : base(message, innerException, data)
        { }
    }
}
