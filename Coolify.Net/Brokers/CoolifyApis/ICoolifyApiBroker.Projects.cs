// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Projects;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalProject>> GetAllProjectsAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalProject> GetProjectByUuidAsync(string projectUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalProject> PostProjectAsync(ExternalProject project, CancellationToken cancellationToken = default);
        ValueTask<ExternalProject> PatchProjectAsync(ExternalProject project, CancellationToken cancellationToken = default);
        ValueTask DeleteProjectAsync(string projectUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalCoolifyEnvironment>> GetAllEnvironmentsAsync(string projectUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalCoolifyEnvironment> PostEnvironmentAsync(string projectUuid, ExternalCoolifyEnvironment environment, CancellationToken cancellationToken = default);
        ValueTask<ExternalCoolifyEnvironment> GetEnvironmentAsync(string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default);
        ValueTask DeleteEnvironmentAsync(string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default);
    }
}
