// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.PrivateKeys;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string PrivateKeysRelativeUrl = "security/keys";

        public async ValueTask<IEnumerable<ExternalPrivateKey>> GetAllPrivateKeysAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalPrivateKey>>(PrivateKeysRelativeUrl, cancellationToken);

        public async ValueTask<ExternalPrivateKey> GetPrivateKeyByUuidAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalPrivateKey>($"{PrivateKeysRelativeUrl}/{privateKeyUuid}", cancellationToken);

        public async ValueTask<ExternalPrivateKey> PostPrivateKeyAsync(
            ExternalPrivateKey privateKey, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalPrivateKey>(PrivateKeysRelativeUrl, privateKey, cancellationToken);

        public async ValueTask<ExternalPrivateKey> PatchPrivateKeyAsync(
            ExternalPrivateKey privateKey, CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalPrivateKey>(
                    $"{PrivateKeysRelativeUrl}/{privateKey.Uuid}", privateKey, cancellationToken);

        public async ValueTask DeletePrivateKeyAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync($"{PrivateKeysRelativeUrl}/{privateKeyUuid}", cancellationToken);
    }
}
