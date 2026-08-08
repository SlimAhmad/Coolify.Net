// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.EnvironmentVariables;

namespace Coolify.Net.Clients.CoolifyServices
{
    /// <summary>Defines the contract for managing Coolify one-click services.</summary>
    public interface ICoolifyServiceClient
    {
        /// <summary>Retrieves all services accessible by the configured team.</summary>
        /// <exception cref="Exceptions.CoolifyServiceClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.CoolifyServiceClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.CoolifyServiceClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<CoolifyService>> RetrieveAllServicesAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a service by its UUID.</summary>
        ValueTask<CoolifyService> RetrieveServiceByUuidAsync(string serviceUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new one-click service.</summary>
        ValueTask<CoolifyService> AddCoolifyServiceAsync(CoolifyService service, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing service.</summary>
        ValueTask<CoolifyService> ModifyCoolifyServiceAsync(CoolifyService service, CancellationToken cancellationToken = default);

        /// <summary>Deletes a service.</summary>
        ValueTask RemoveCoolifyServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists all environment variables for a service.</summary>
        ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllServiceEnvVarsAsync(string serviceUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new environment variable for a service.</summary>
        ValueTask<EnvironmentVariable> AddServiceEnvVarAsync(string serviceUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing environment variable for a service.</summary>
        ValueTask<EnvironmentVariable> ModifyServiceEnvVarAsync(string serviceUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);

        /// <summary>Bulk creates or updates environment variables for a service.</summary>
        ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkServiceEnvVarsAsync(string serviceUuid, IEnumerable<EnvironmentVariable> environmentVariables, CancellationToken cancellationToken = default);

        /// <summary>Deletes an environment variable from a service.</summary>
        ValueTask RemoveServiceEnvVarAsync(string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default);

        /// <summary>Starts (deploys) the service.</summary>
        ValueTask StartServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);

        /// <summary>Stops the service.</summary>
        ValueTask StopServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);

        /// <summary>Restarts the service.</summary>
        ValueTask RestartServiceAsync(string serviceUuid, CancellationToken cancellationToken = default);
    }
}
