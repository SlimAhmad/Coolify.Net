// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions
{
    public class TimeoutPrivateKeyProcessingException : Xeption
    {
        public TimeoutPrivateKeyProcessingException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
