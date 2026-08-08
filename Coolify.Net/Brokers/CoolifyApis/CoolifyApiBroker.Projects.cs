// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Projects;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string ProjectsRelativeUrl = "projects";

        public async ValueTask<IEnumerable<ExternalProject>> GetAllProjectsAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalProject>>(ProjectsRelativeUrl, cancellationToken);

        public async ValueTask<ExternalProject> GetProjectByUuidAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalProject>($"{ProjectsRelativeUrl}/{projectUuid}", cancellationToken);

        public async ValueTask<ExternalProject> PostProjectAsync(
            ExternalProject project, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalProject>(ProjectsRelativeUrl, project, cancellationToken);

        public async ValueTask<ExternalProject> PatchProjectAsync(
            ExternalProject project, CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalProject>($"{ProjectsRelativeUrl}/{project.Uuid}", project, cancellationToken);

        public async ValueTask DeleteProjectAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync($"{ProjectsRelativeUrl}/{projectUuid}", cancellationToken);

        public async ValueTask<IEnumerable<ExternalCoolifyEnvironment>> GetAllEnvironmentsAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalCoolifyEnvironment>>(
                    $"{ProjectsRelativeUrl}/{projectUuid}/environments", cancellationToken);

        public async ValueTask<ExternalCoolifyEnvironment> PostEnvironmentAsync(
            string projectUuid, ExternalCoolifyEnvironment environment, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalCoolifyEnvironment>(
                    $"{ProjectsRelativeUrl}/{projectUuid}/environments", environment, cancellationToken);

        public async ValueTask<ExternalCoolifyEnvironment> GetEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalCoolifyEnvironment>(
                    $"{ProjectsRelativeUrl}/{projectUuid}/{environmentNameOrUuid}", cancellationToken);

        public async ValueTask DeleteEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync(
                    $"{ProjectsRelativeUrl}/{projectUuid}/environments/{environmentNameOrUuid}", cancellationToken);
    }
}
