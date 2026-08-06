// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Services.Foundations.Servers;

namespace Coolify.Resource.Manager.Services.Processings.Servers
{
    public partial class ServerProcessingService : IServerProcessingService
    {
        private readonly IServerService serverService;
        private readonly ILoggingBroker loggingBroker;

        public ServerProcessingService(
            IServerService serverService,
            ILoggingBroker loggingBroker)
        {
            this.serverService = serverService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Server>> RetrieveAllServersAsync(CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                return await this.serverService.RetrieveAllServersAsync(cancellationToken);
            });

        public ValueTask<Server> RetrieveServerByUuidAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServerUuid(serverUuid);

                    return await this.serverService.RetrieveServerByUuidAsync(serverUuid, cancellationToken);
                });

        public ValueTask<Server> AddServerAsync(Server server, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateServerIsNotNull(server);

                return await this.serverService.AddServerAsync(server, cancellationToken);
            });

        public ValueTask<Server> ModifyServerAsync(Server server, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateServerIsNotNull(server);

                return await this.serverService.ModifyServerAsync(server, cancellationToken);
            });

        public ValueTask RemoveServerAsync(string serverUuid, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateServerUuid(serverUuid);

                await this.serverService.RemoveServerAsync(serverUuid, cancellationToken);
            });

        public ValueTask<Server> RetrieveServerValidationAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServerUuid(serverUuid);

                    return await this.serverService.RetrieveServerValidationAsync(serverUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<object>> RetrieveServerResourcesAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServerUuid(serverUuid);

                    return await this.serverService.RetrieveServerResourcesAsync(serverUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<string>> RetrieveServerDomainsAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServerUuid(serverUuid);

                    return await this.serverService.RetrieveServerDomainsAsync(serverUuid, cancellationToken);
                });
    }
}
