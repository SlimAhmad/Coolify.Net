// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Projects;
using Coolify.Net.Models.Foundations.Projects;

namespace Coolify.Net.Services.Foundations.Projects
{
    public partial class ProjectService : IProjectService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public ProjectService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Project>> RetrieveAllProjectsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalProject> externalProjects =
                        await this.coolifyApiBroker.GetAllProjectsAsync(cancellationToken);

                    return externalProjects.Select(ConvertToProject);
                });

        public ValueTask<Project> RetrieveProjectByUuidAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);

                    ExternalProject externalProject =
                        await this.coolifyApiBroker.GetProjectByUuidAsync(projectUuid, cancellationToken);

                    return ConvertToProject(externalProject);
                });

        public ValueTask<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateProject(project);

                ExternalProject externalProject = ConvertToExternalProject(project);

                ExternalProject returnedExternalProject =
                    await this.coolifyApiBroker.PostProjectAsync(externalProject, cancellationToken);

                return ConvertToProject(returnedExternalProject);
            });

        public ValueTask<Project> ModifyProjectAsync(Project project, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateProject(project);

                ExternalProject externalProject = ConvertToExternalProject(project);

                ExternalProject returnedExternalProject =
                    await this.coolifyApiBroker.PatchProjectAsync(externalProject, cancellationToken);

                return ConvertToProject(returnedExternalProject);
            });

        public ValueTask RemoveProjectAsync(string projectUuid, CancellationToken cancellationToken = default) =>
            TryCatch(async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                ValidateProjectUuid(projectUuid);
                await this.coolifyApiBroker.DeleteProjectAsync(projectUuid, cancellationToken);
            });

        public ValueTask<IEnumerable<CoolifyEnvironment>> RetrieveAllEnvironmentsAsync(
            string projectUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);

                    IEnumerable<ExternalCoolifyEnvironment> externalEnvironments =
                        await this.coolifyApiBroker.GetAllEnvironmentsAsync(projectUuid, cancellationToken);

                    return externalEnvironments.Select(ConvertToEnvironment);
                });

        public ValueTask<CoolifyEnvironment> AddEnvironmentAsync(
            string projectUuid, CoolifyEnvironment environment, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);
                    ValidateEnvironment(environment);

                    ExternalCoolifyEnvironment externalEnvironment = ConvertToExternalEnvironment(environment);

                    ExternalCoolifyEnvironment returnedExternalEnvironment =
                        await this.coolifyApiBroker.PostEnvironmentAsync(
                            projectUuid, externalEnvironment, cancellationToken);

                    return ConvertToEnvironment(returnedExternalEnvironment);
                });

        public ValueTask<CoolifyEnvironment> RetrieveEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);
                    ValidateEnvironmentNameOrUuid(environmentNameOrUuid);

                    ExternalCoolifyEnvironment externalEnvironment =
                        await this.coolifyApiBroker.GetEnvironmentAsync(
                            projectUuid, environmentNameOrUuid, cancellationToken);

                    return ConvertToEnvironment(externalEnvironment);
                });

        public ValueTask RemoveEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateProjectUuid(projectUuid);
                    ValidateEnvironmentNameOrUuid(environmentNameOrUuid);

                    await this.coolifyApiBroker.DeleteEnvironmentAsync(
                        projectUuid, environmentNameOrUuid, cancellationToken);
                });

        // ---- Conversion helpers ----

        private static Project ConvertToProject(ExternalProject externalProject) =>
            new Project
            {
                Uuid = externalProject.Uuid,
                Name = externalProject.Name,
                Description = externalProject.Description,
                CreatedAt = externalProject.CreatedAt,
                UpdatedAt = externalProject.UpdatedAt
            };

        private static ExternalProject ConvertToExternalProject(Project project) =>
            new ExternalProject
            {
                Uuid = project.Uuid,
                Name = project.Name,
                Description = project.Description
            };

        private static CoolifyEnvironment ConvertToEnvironment(ExternalCoolifyEnvironment externalEnvironment) =>
            new CoolifyEnvironment
            {
                Uuid = externalEnvironment.Uuid,
                Name = externalEnvironment.Name,
                Description = externalEnvironment.Description,
                ProjectUuid = externalEnvironment.ProjectUuid,
                CreatedAt = externalEnvironment.CreatedAt,
                UpdatedAt = externalEnvironment.UpdatedAt
            };

        private static ExternalCoolifyEnvironment ConvertToExternalEnvironment(CoolifyEnvironment environment) =>
            new ExternalCoolifyEnvironment
            {
                Uuid = environment.Uuid,
                Name = environment.Name,
                Description = environment.Description,
                ProjectUuid = environment.ProjectUuid
            };
    }
}
