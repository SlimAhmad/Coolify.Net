// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions
{
    public class PrivateKeyProcessingDependencyException : Xeption
    {
        public PrivateKeyProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
