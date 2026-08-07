// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Systems;

namespace Coolify.Resource.Manager.Clients.Systems
{
    /// <summary>Defines the contract for reading Coolify instance/system status.</summary>
    public interface ISystemClient
    {
        /// <summary>Retrieves the Coolify instance version.</summary>
        /// <exception cref="Exceptions.SystemClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.SystemClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.SystemClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<SystemInfo> RetrieveVersionAsync(CancellationToken cancellationToken = default);

        /// <summary>Checks whether the Coolify instance is healthy.</summary>
        ValueTask<bool> CheckHealthAsync(CancellationToken cancellationToken = default);

        /// <summary>Enables the Coolify API.</summary>
        ValueTask<bool> EnableApiAsync(CancellationToken cancellationToken = default);

        /// <summary>Disables the Coolify API.</summary>
        ValueTask<bool> DisableApiAsync(CancellationToken cancellationToken = default);
    }
}
