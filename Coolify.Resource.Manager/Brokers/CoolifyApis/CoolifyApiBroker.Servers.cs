// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Servers;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string ServersRelativeUrl = "servers";

        public async ValueTask<IEnumerable<ExternalServer>> GetAllServersAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalServer>>(ServersRelativeUrl, cancellationToken);

        public async ValueTask<ExternalServer> GetServerByUuidAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalServer>($"{ServersRelativeUrl}/{serverUuid}", cancellationToken);

        public async ValueTask<ExternalServer> PostServerAsync(
            ExternalServer server, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalServer>(ServersRelativeUrl, server, cancellationToken);

        public async ValueTask<ExternalServer> PatchServerAsync(
            ExternalServer server, CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalServer>($"{ServersRelativeUrl}/{server.Uuid}", server, cancellationToken);

        public async ValueTask DeleteServerAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync($"{ServersRelativeUrl}/{serverUuid}", cancellationToken);

        public async ValueTask<ExternalServer> GetValidateServerAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalServer>($"{ServersRelativeUrl}/{serverUuid}/validate", cancellationToken);

        public async ValueTask<IEnumerable<object>> GetServerResourcesAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<object>>($"{ServersRelativeUrl}/{serverUuid}/resources", cancellationToken);

        public async ValueTask<IEnumerable<string>> GetServerDomainsAsync(
            string serverUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<string>>($"{ServersRelativeUrl}/{serverUuid}/domains", cancellationToken);
    }
}
