// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Services.Foundations.Projects;

namespace Coolify.Net.Services.Processings.Projects
{
    public partial class ProjectProcessingService : IProjectProcessingService
    {
        private readonly IProjectService projectService;
        private readonly ILoggingBroker loggingBroker;

        public ProjectProcessingService(
            IProjectService projectService,
            ILoggingBroker loggingBroker)
        {
            this.projectService = projectService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Project>> RetrieveAllProjectsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.projectService.RetrieveAllProjectsAsync(cancellationToken);
                });

        public ValueTask<Project> RetrieveProjectByUuidAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);

                    return await this.projectService.RetrieveProjectByUuidAsync(projectUuid, cancellationToken);
                });

        public ValueTask<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateProjectIsNotNull(project);

                return await this.projectService.AddProjectAsync(project, cancellationToken);
            });

        public ValueTask<Project> ModifyProjectAsync(Project project, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateProjectIsNotNull(project);

                return await this.projectService.ModifyProjectAsync(project, cancellationToken);
            });

        public ValueTask RemoveProjectAsync(string projectUuid, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateProjectUuid(projectUuid);

                await this.projectService.RemoveProjectAsync(projectUuid, cancellationToken);
            });

        public ValueTask<IEnumerable<CoolifyEnvironment>> RetrieveAllEnvironmentsAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);

                    return await this.projectService.RetrieveAllEnvironmentsAsync(projectUuid, cancellationToken);
                });

        public ValueTask<CoolifyEnvironment> AddEnvironmentAsync(
            string projectUuid, CoolifyEnvironment environment, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);
                    ValidateEnvironmentIsNotNull(environment);

                    return await this.projectService.AddEnvironmentAsync(projectUuid, environment, cancellationToken);
                });

        public ValueTask<CoolifyEnvironment> RetrieveEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);
                    ValidateEnvironmentNameOrUuid(environmentNameOrUuid);

                    return await this.projectService.RetrieveEnvironmentAsync(
                        projectUuid, environmentNameOrUuid, cancellationToken);
                });

        public ValueTask RemoveEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);
                    ValidateEnvironmentNameOrUuid(environmentNameOrUuid);

                    await this.projectService.RemoveEnvironmentAsync(
                        projectUuid, environmentNameOrUuid, cancellationToken);
                });
    }
}
