// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.PrivateKeys;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalPrivateKey>> GetAllPrivateKeysAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalPrivateKey> GetPrivateKeyByUuidAsync(string privateKeyUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalPrivateKey> PostPrivateKeyAsync(ExternalPrivateKey privateKey, CancellationToken cancellationToken = default);
        ValueTask<ExternalPrivateKey> PatchPrivateKeyAsync(ExternalPrivateKey privateKey, CancellationToken cancellationToken = default);
        ValueTask DeletePrivateKeyAsync(string privateKeyUuid, CancellationToken cancellationToken = default);
    }
}
