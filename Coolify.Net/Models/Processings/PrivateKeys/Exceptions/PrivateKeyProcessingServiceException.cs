// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.PrivateKeys.Exceptions
{
    public class PrivateKeyProcessingServiceException : Xeption
    {
        public PrivateKeyProcessingServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
