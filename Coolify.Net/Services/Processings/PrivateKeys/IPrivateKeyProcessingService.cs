// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;

namespace Coolify.Net.Services.Processings.PrivateKeys
{
    public interface IPrivateKeyProcessingService
    {
        ValueTask<IEnumerable<PrivateKey>> RetrieveAllPrivateKeysAsync(CancellationToken cancellationToken = default);
        ValueTask<PrivateKey> RetrievePrivateKeyByUuidAsync(string privateKeyUuid, CancellationToken cancellationToken = default);
        ValueTask<PrivateKey> AddPrivateKeyAsync(PrivateKey privateKey, CancellationToken cancellationToken = default);
        ValueTask<PrivateKey> ModifyPrivateKeyAsync(PrivateKey privateKey, CancellationToken cancellationToken = default);
        ValueTask RemovePrivateKeyAsync(string privateKeyUuid, CancellationToken cancellationToken = default);
    }
}
