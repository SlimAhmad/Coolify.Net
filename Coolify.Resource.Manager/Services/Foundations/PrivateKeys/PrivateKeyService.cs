// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.PrivateKeys;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;

namespace Coolify.Resource.Manager.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyService : IPrivateKeyService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public PrivateKeyService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<PrivateKey>> RetrieveAllPrivateKeysAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalPrivateKey> externalPrivateKeys =
                        await this.coolifyApiBroker.GetAllPrivateKeysAsync(cancellationToken);

                    return externalPrivateKeys.Select(ConvertToPrivateKey);
                });

        public ValueTask<PrivateKey> RetrievePrivateKeyByUuidAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKeyUuid(privateKeyUuid);

                    ExternalPrivateKey externalPrivateKey =
                        await this.coolifyApiBroker.GetPrivateKeyByUuidAsync(privateKeyUuid, cancellationToken);

                    return ConvertToPrivateKey(externalPrivateKey);
                });

        public ValueTask<PrivateKey> AddPrivateKeyAsync(
            PrivateKey privateKey, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKey(privateKey);

                    ExternalPrivateKey externalPrivateKey = ConvertToExternalPrivateKey(privateKey);

                    ExternalPrivateKey returnedExternalPrivateKey =
                        await this.coolifyApiBroker.PostPrivateKeyAsync(externalPrivateKey, cancellationToken);

                    return ConvertToPrivateKey(returnedExternalPrivateKey);
                });

        public ValueTask<PrivateKey> ModifyPrivateKeyAsync(
            PrivateKey privateKey, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKey(privateKey);

                    ExternalPrivateKey externalPrivateKey = ConvertToExternalPrivateKey(privateKey);

                    ExternalPrivateKey returnedExternalPrivateKey =
                        await this.coolifyApiBroker.PatchPrivateKeyAsync(externalPrivateKey, cancellationToken);

                    return ConvertToPrivateKey(returnedExternalPrivateKey);
                });

        public ValueTask RemovePrivateKeyAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKeyUuid(privateKeyUuid);
                    await this.coolifyApiBroker.DeletePrivateKeyAsync(privateKeyUuid, cancellationToken);
                });

        // ---- Conversion helpers ----

        private static PrivateKey ConvertToPrivateKey(ExternalPrivateKey externalPrivateKey) =>
            new PrivateKey
            {
                Uuid = externalPrivateKey.Uuid,
                Name = externalPrivateKey.Name,
                Description = externalPrivateKey.Description,
                PrivateKeyValue = externalPrivateKey.PrivateKeyValue,
                TeamId = externalPrivateKey.TeamId,
                CreatedAt = externalPrivateKey.CreatedAt,
                UpdatedAt = externalPrivateKey.UpdatedAt
            };

        private static ExternalPrivateKey ConvertToExternalPrivateKey(PrivateKey privateKey) =>
            new ExternalPrivateKey
            {
                Uuid = privateKey.Uuid,
                Name = privateKey.Name,
                Description = privateKey.Description,
                PrivateKeyValue = privateKey.PrivateKeyValue,
                TeamId = privateKey.TeamId
            };
    }
}
