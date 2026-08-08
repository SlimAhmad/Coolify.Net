// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;

namespace Coolify.Net.Clients.PrivateKeys
{
    /// <summary>Defines the contract for managing Coolify private keys.</summary>
    public interface IPrivateKeyClient
    {
        /// <summary>Retrieves all private keys accessible by the configured team.</summary>
        /// <exception cref="Exceptions.PrivateKeyClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.PrivateKeyClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.PrivateKeyClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<PrivateKey>> RetrieveAllPrivateKeysAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a private key by its UUID.</summary>
        ValueTask<PrivateKey> RetrievePrivateKeyByUuidAsync(string privateKeyUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new private key.</summary>
        ValueTask<PrivateKey> AddPrivateKeyAsync(PrivateKey privateKey, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing private key.</summary>
        ValueTask<PrivateKey> ModifyPrivateKeyAsync(PrivateKey privateKey, CancellationToken cancellationToken = default);

        /// <summary>Deletes a private key.</summary>
        ValueTask RemovePrivateKeyAsync(string privateKeyUuid, CancellationToken cancellationToken = default);
    }
}
