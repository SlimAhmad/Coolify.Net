// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Xeptions;

namespace Coolify.Net.Models.Processings.PrivateKeys.Exceptions
{
    public class NullPrivateKeyProcessingException : Xeption
    {
        public NullPrivateKeyProcessingException(string message)
            : base(message)
        { }
    }
}
