// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;

namespace Coolify.Resource.Manager.Services.Foundations.Servers
{
    public interface IServerService
    {
        ValueTask<IEnumerable<Server>> RetrieveAllServersAsync();
        ValueTask<Server> RetrieveServerByUuidAsync(string serverUuid);
        ValueTask<Server> AddServerAsync(Server server);
        ValueTask<Server> ModifyServerAsync(Server server);
        ValueTask RemoveServerAsync(string serverUuid);
        ValueTask<Server> RetrieveServerValidationAsync(string serverUuid);
        ValueTask<IEnumerable<object>> RetrieveServerResourcesAsync(string serverUuid);
        ValueTask<IEnumerable<string>> RetrieveServerDomainsAsync(string serverUuid);
    }
}
