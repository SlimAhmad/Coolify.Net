// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions;

namespace Coolify.Resource.Manager.Services.Processings.PrivateKeys
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
