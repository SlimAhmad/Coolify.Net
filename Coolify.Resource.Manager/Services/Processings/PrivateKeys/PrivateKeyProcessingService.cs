// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Services.Foundations.PrivateKeys;

namespace Coolify.Resource.Manager.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingService : IPrivateKeyProcessingService
    {
        private readonly IPrivateKeyService privateKeyService;
        private readonly ILoggingBroker loggingBroker;

        public PrivateKeyProcessingService(
            IPrivateKeyService privateKeyService,
            ILoggingBroker loggingBroker)
        {
            this.privateKeyService = privateKeyService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<PrivateKey>> RetrieveAllPrivateKeysAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.privateKeyService.RetrieveAllPrivateKeysAsync(cancellationToken);
                });

        public ValueTask<PrivateKey> RetrievePrivateKeyByUuidAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKeyUuid(privateKeyUuid);

                    return await this.privateKeyService.RetrievePrivateKeyByUuidAsync(privateKeyUuid, cancellationToken);
                });

        public ValueTask<PrivateKey> AddPrivateKeyAsync(
            PrivateKey privateKey, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKeyIsNotNull(privateKey);

                    return await this.privateKeyService.AddPrivateKeyAsync(privateKey, cancellationToken);
                });

        public ValueTask<PrivateKey> ModifyPrivateKeyAsync(
            PrivateKey privateKey, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKeyIsNotNull(privateKey);

                    return await this.privateKeyService.ModifyPrivateKeyAsync(privateKey, cancellationToken);
                });

        public ValueTask RemovePrivateKeyAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidatePrivateKeyUuid(privateKeyUuid);

                    await this.privateKeyService.RemovePrivateKeyAsync(privateKeyUuid, cancellationToken);
                });
    }
}
