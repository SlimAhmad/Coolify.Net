// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.CoolifyServices;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalCoolifyService>> GetAllServicesAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalCoolifyService> GetServiceByUuidAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalCoolifyService> PostServiceAsync(ExternalCoolifyService service, CancellationToken cancellationToken = default);
        ValueTask<ExternalCoolifyService> PatchServiceAsync(ExternalCoolifyService service, CancellationToken cancellationToken = default);
        ValueTask DeleteServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalEnvironmentVariable>> GetServiceEnvVarsAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalEnvironmentVariable> PostServiceEnvVarAsync(string serviceUuid, ExternalEnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<ExternalEnvironmentVariable> PatchServiceEnvVarAsync(string serviceUuid, ExternalEnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalEnvironmentVariable>> PatchServiceEnvVarsBulkAsync(string serviceUuid, IEnumerable<ExternalEnvironmentVariable> environmentVariables, CancellationToken cancellationToken = default);
        ValueTask DeleteServiceEnvVarAsync(string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default);
        ValueTask PostServiceStartAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask PostServiceStopAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask PostServiceRestartAsync(string serviceUuid, CancellationToken cancellationToken = default);
    }
}
