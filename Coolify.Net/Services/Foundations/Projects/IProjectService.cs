// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;

namespace Coolify.Net.Services.Foundations.Projects
{
    public interface IProjectService
    {
        ValueTask<IEnumerable<Project>> RetrieveAllProjectsAsync(CancellationToken cancellationToken = default);
        ValueTask<Project> RetrieveProjectByUuidAsync(string projectUuid, CancellationToken cancellationToken = default);
        ValueTask<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default);
        ValueTask<Project> ModifyProjectAsync(Project project, CancellationToken cancellationToken = default);
        ValueTask RemoveProjectAsync(string projectUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<CoolifyEnvironment>> RetrieveAllEnvironmentsAsync(string projectUuid, CancellationToken cancellationToken = default);
        ValueTask<CoolifyEnvironment> AddEnvironmentAsync(string projectUuid, CoolifyEnvironment environment, CancellationToken cancellationToken = default);
        ValueTask<CoolifyEnvironment> RetrieveEnvironmentAsync(string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default);
        ValueTask RemoveEnvironmentAsync(string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default);
    }
}
