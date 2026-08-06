// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Projects;

namespace Coolify.Resource.Manager.Clients.Projects
{
    /// <summary>Defines the contract for managing Coolify projects and their environments.</summary>
    public interface IProjectClient
    {
        /// <summary>Retrieves all projects accessible by the configured team.</summary>
        /// <exception cref="Exceptions.ProjectClientValidationException">Thrown on invalid parameters.</exception>
        /// <exception cref="Exceptions.ProjectClientDependencyException">Thrown on API errors.</exception>
        /// <exception cref="Exceptions.ProjectClientServiceException">Thrown on unexpected errors.</exception>
        ValueTask<IEnumerable<Project>> RetrieveAllProjectsAsync(CancellationToken cancellationToken = default);

        /// <summary>Retrieves a project by its UUID.</summary>
        ValueTask<Project> RetrieveProjectByUuidAsync(string projectUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new project.</summary>
        ValueTask<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>Updates an existing project.</summary>
        ValueTask<Project> ModifyProjectAsync(Project project, CancellationToken cancellationToken = default);

        /// <summary>Deletes a project.</summary>
        ValueTask RemoveProjectAsync(string projectUuid, CancellationToken cancellationToken = default);

        /// <summary>Lists all environments within a project.</summary>
        ValueTask<IEnumerable<CoolifyEnvironment>> RetrieveAllEnvironmentsAsync(string projectUuid, CancellationToken cancellationToken = default);

        /// <summary>Creates a new environment within a project.</summary>
        ValueTask<CoolifyEnvironment> AddEnvironmentAsync(string projectUuid, CoolifyEnvironment environment, CancellationToken cancellationToken = default);

        /// <summary>Retrieves an environment by name or UUID.</summary>
        ValueTask<CoolifyEnvironment> RetrieveEnvironmentAsync(string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default);

        /// <summary>Deletes an environment from a project.</summary>
        ValueTask RemoveEnvironmentAsync(string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default);
    }
}
