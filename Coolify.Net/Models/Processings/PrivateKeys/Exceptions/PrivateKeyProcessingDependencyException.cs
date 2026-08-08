// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.PrivateKeys.Exceptions
{
    public class PrivateKeyProcessingDependencyException : Xeption
    {
        public PrivateKeyProcessingDependencyException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
