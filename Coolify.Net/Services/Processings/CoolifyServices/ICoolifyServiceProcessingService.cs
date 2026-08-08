// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.EnvironmentVariables;

namespace Coolify.Net.Services.Processings.CoolifyServices
{
    public interface ICoolifyServiceProcessingService
    {
        ValueTask<IEnumerable<CoolifyService>> RetrieveAllServicesAsync(CancellationToken cancellationToken = default);
        ValueTask<CoolifyService> RetrieveServiceByUuidAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask<CoolifyService> AddCoolifyServiceAsync(CoolifyService service, CancellationToken cancellationToken = default);
        ValueTask<CoolifyService> ModifyCoolifyServiceAsync(CoolifyService service, CancellationToken cancellationToken = default);
        ValueTask RemoveCoolifyServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllServiceEnvVarsAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask<EnvironmentVariable> AddServiceEnvVarAsync(string serviceUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<EnvironmentVariable> ModifyServiceEnvVarAsync(string serviceUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkServiceEnvVarsAsync(string serviceUuid, IEnumerable<EnvironmentVariable> environmentVariables, CancellationToken cancellationToken = default);
        ValueTask RemoveServiceEnvVarAsync(string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default);
        ValueTask StartServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask StopServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);
        ValueTask RestartServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);
    }
}
