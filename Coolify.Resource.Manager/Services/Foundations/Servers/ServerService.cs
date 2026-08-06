// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers;

namespace Coolify.Resource.Manager.Services.Foundations.Servers
{
    public partial class ServerService : IServerService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public ServerService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Server>> RetrieveAllServersAsync() =>
            TryCatch(async () =>
            {
                IEnumerable<ExternalServer> externalServers =
                    await this.coolifyApiBroker.GetAllServersAsync();

                return externalServers.Select(ConvertToServer);
            });

        public ValueTask<Server> RetrieveServerByUuidAsync(string serverUuid) =>
            TryCatch(async () =>
            {
                ValidateServerUuid(serverUuid);

                ExternalServer externalServer =
                    await this.coolifyApiBroker.GetServerByUuidAsync(serverUuid);

                return ConvertToServer(externalServer);
            });

        public ValueTask<Server> AddServerAsync(Server server) =>
            TryCatch(async () =>
            {
                ValidateServer(server);

                ExternalServer externalServer = ConvertToExternalServer(server);

                ExternalServer returnedExternalServer =
                    await this.coolifyApiBroker.PostServerAsync(externalServer);

                return ConvertToServer(returnedExternalServer);
            });

        public ValueTask<Server> ModifyServerAsync(Server server) =>
            TryCatch(async () =>
            {
                ValidateServer(server);

                ExternalServer externalServer = ConvertToExternalServer(server);

                ExternalServer returnedExternalServer =
                    await this.coolifyApiBroker.PatchServerAsync(externalServer);

                return ConvertToServer(returnedExternalServer);
            });

        public ValueTask RemoveServerAsync(string serverUuid) =>
            TryCatch(async () =>
            {
                ValidateServerUuid(serverUuid);
                await this.coolifyApiBroker.DeleteServerAsync(serverUuid);
            });

        public ValueTask<Server> RetrieveServerValidationAsync(string serverUuid) =>
            TryCatch(async () =>
            {
                ValidateServerUuid(serverUuid);

                ExternalServer externalServer =
                    await this.coolifyApiBroker.GetValidateServerAsync(serverUuid);

                return ConvertToServer(externalServer);
            });

        public ValueTask<IEnumerable<object>> RetrieveServerResourcesAsync(string serverUuid) =>
            TryCatch(async () =>
            {
                ValidateServerUuid(serverUuid);

                return await this.coolifyApiBroker.GetServerResourcesAsync(serverUuid);
            });

        public ValueTask<IEnumerable<string>> RetrieveServerDomainsAsync(string serverUuid) =>
            TryCatch(async () =>
            {
                ValidateServerUuid(serverUuid);

                return await this.coolifyApiBroker.GetServerDomainsAsync(serverUuid);
            });

        // ---- Conversion helpers ----

        private static Server ConvertToServer(ExternalServer externalServer) =>
            new Server
            {
                Uuid = externalServer.Uuid,
                Name = externalServer.Name,
                Description = externalServer.Description,
                Ip = externalServer.Ip,
                User = externalServer.User,
                Port = externalServer.Port,
                PrivateKeyUuid = externalServer.PrivateKeyUuid,
                ProxyEnabled = externalServer.ProxyEnabled,
                ProxyType = externalServer.ProxyType,
                IsReachable = externalServer.IsReachable,
                IsUsable = externalServer.IsUsable,
                CreatedAt = externalServer.CreatedAt,
                UpdatedAt = externalServer.UpdatedAt,

                Settings = externalServer.Settings is null ? null : new ServerSetting
                {
                    IsBuildServer = externalServer.Settings.IsBuildServer,
                    IsSwarmManager = externalServer.Settings.IsSwarmManager,
                    IsSwarmWorker = externalServer.Settings.IsSwarmWorker,
                    SentinelEnabled = externalServer.Settings.SentinelEnabled,
                    SentinelToken = externalServer.Settings.SentinelToken,
                    IsReachable = externalServer.Settings.IsReachable,
                    IsUsable = externalServer.Settings.IsUsable,
                    CreatedAt = externalServer.Settings.CreatedAt,
                    UpdatedAt = externalServer.Settings.UpdatedAt
                }
            };

        private static ExternalServer ConvertToExternalServer(Server server) =>
            new ExternalServer
            {
                Uuid = server.Uuid,
                Name = server.Name,
                Description = server.Description,
                Ip = server.Ip,
                User = server.User,
                Port = server.Port,
                PrivateKeyUuid = server.PrivateKeyUuid,
                ProxyEnabled = server.ProxyEnabled,
                ProxyType = server.ProxyType
            };
    }
}
