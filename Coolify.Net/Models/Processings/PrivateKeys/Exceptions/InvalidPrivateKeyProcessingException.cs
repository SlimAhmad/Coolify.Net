// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.PrivateKeys.Exceptions
{
    public class InvalidPrivateKeyProcessingException : Xeption
    {
        public InvalidPrivateKeyProcessingException(string message)
            : base(message)
        { }
    }
}
