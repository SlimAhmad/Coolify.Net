// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.PrivateKeys.Exceptions
{
    public class PrivateKeyProcessingDependencyValidationException : Xeption
    {
        public PrivateKeyProcessingDependencyValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
