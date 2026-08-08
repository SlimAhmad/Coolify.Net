// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Processings.PrivateKeys.Exceptions;

namespace Coolify.Net.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingService
    {
        private static void ValidatePrivateKeyIsNotNull(PrivateKey privateKey)
        {
            if (privateKey is null)
            {
                throw new NullPrivateKeyProcessingException(message: "Private key is null.");
            }
        }

        private static void ValidatePrivateKeyUuid(string privateKeyUuid)
        {
            if (string.IsNullOrWhiteSpace(privateKeyUuid))
            {
                throw new InvalidPrivateKeyProcessingException(message: "Private key uuid is invalid.");
            }
        }
    }
}
