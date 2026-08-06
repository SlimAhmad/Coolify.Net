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

        public async ValueTask<IEnumerable<ExternalServer>> GetAllServersAsync() =>
            await GetAsync<IEnumerable<ExternalServer>>(ServersRelativeUrl);

        public async ValueTask<ExternalServer> GetServerByUuidAsync(string serverUuid) =>
            await GetAsync<ExternalServer>($"{ServersRelativeUrl}/{serverUuid}");

        public async ValueTask<ExternalServer> PostServerAsync(ExternalServer server) =>
            await PostAsync<ExternalServer>(ServersRelativeUrl, server);

        public async ValueTask<ExternalServer> PatchServerAsync(ExternalServer server) =>
            await PatchAsync<ExternalServer>($"{ServersRelativeUrl}/{server.Uuid}", server);

        public async ValueTask DeleteServerAsync(string serverUuid) =>
            await DeleteAsync($"{ServersRelativeUrl}/{serverUuid}");

        public async ValueTask<ExternalServer> GetValidateServerAsync(string serverUuid) =>
            await GetAsync<ExternalServer>($"{ServersRelativeUrl}/{serverUuid}/validate");

        public async ValueTask<IEnumerable<object>> GetServerResourcesAsync(string serverUuid) =>
            await GetAsync<IEnumerable<object>>($"{ServersRelativeUrl}/{serverUuid}/resources");

        public async ValueTask<IEnumerable<string>> GetServerDomainsAsync(string serverUuid) =>
            await GetAsync<IEnumerable<string>>($"{ServersRelativeUrl}/{serverUuid}/domains");
    }
}
