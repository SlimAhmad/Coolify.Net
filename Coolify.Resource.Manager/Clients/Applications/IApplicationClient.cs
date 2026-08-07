// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;

namespace Coolify.Resource.Manager.Clients.Applications
{
    /// <summary>Defines the contract for managing Coolify applications.</summary>
    public interface IApplicationClient
    {
        /// <summary>Retrieves all applications accessible by the configured team.</summary>
        /// <exception cref="Exceptions.ApplicationClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.ApplicationClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.ApplicationClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<Application>> RetrieveAllApplicationsAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves an application by its UUID.</summary>
        ValueTask<Application> RetrieveApplicationByUuidAsync(string applicationUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new application from a public Git repository.</summary>
        ValueTask<Application> AddPublicApplicationAsync(Application application, CancellationToken cancellationToken = default);

        /// <summary>Creates a new application from a private repository via a GitHub App.</summary>
        ValueTask<Application> AddPrivateGithubAppApplicationAsync(Application application, CancellationToken cancellationToken = default);

        /// <summary>Creates a new application from a private repository via a deploy key.</summary>
        ValueTask<Application> AddPrivateDeployKeyApplicationAsync(Application application, CancellationToken cancellationToken = default);

        /// <summary>Creates a new application from a Dockerfile.</summary>
        ValueTask<Application> AddDockerfileApplicationAsync(Application application, CancellationToken cancellationToken = default);

        /// <summary>Creates a new application from a Docker image.</summary>
        ValueTask<Application> AddDockerImageApplicationAsync(Application application, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing application.</summary>
        ValueTask<Application> ModifyApplicationAsync(Application application, CancellationToken cancellationToken = default);

        /// <summary>Deletes an application.</summary>
        ValueTask RemoveApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists all environment variables for an application.</summary>
        ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllApplicationEnvVarsAsync(string applicationUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new environment variable for an application.</summary>
        ValueTask<EnvironmentVariable> AddApplicationEnvVarAsync(string applicationUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing environment variable for an application.</summary>
        ValueTask<EnvironmentVariable> ModifyApplicationEnvVarAsync(string applicationUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);

        /// <summary>Bulk creates or updates environment variables for an application.</summary>
        ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkApplicationEnvVarsAsync(string applicationUuid, IEnumerable<EnvironmentVariable> environmentVariables, CancellationToken cancellationToken = default);

        /// <summary>Deletes an environment variable from an application.</summary>
        ValueTask RemoveApplicationEnvVarAsync(string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default);

        /// <summary>Starts (deploys) the application.</summary>
        ValueTask StartApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);

        /// <summary>Stops the application.</summary>
        ValueTask StopApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);

        /// <summary>Restarts the application.</summary>
        ValueTask RestartApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
