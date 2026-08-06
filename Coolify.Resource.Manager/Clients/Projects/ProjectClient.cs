// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Projects.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions;
using Coolify.Resource.Manager.Services.Foundations.Projects;
using Xeptions;

namespace Coolify.Resource.Manager.Clients.Projects
{
    /// <summary>Provides project and environment management operations.</summary>
    internal class ProjectClient : IProjectClient
    {
        private readonly IProjectService projectService;

        public ProjectClient(IProjectService projectService) =>
            this.projectService = projectService;

        public async ValueTask<IEnumerable<Project>> RetrieveAllProjectsAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.RetrieveAllProjectsAsync(cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Project> RetrieveProjectByUuidAsync(
            string projectUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.RetrieveProjectByUuidAsync(projectUuid, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Project> AddProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.AddProjectAsync(project, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Project> ModifyProjectAsync(Project project, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.ModifyProjectAsync(project, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveProjectAsync(string projectUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.projectService.RemoveProjectAsync(projectUuid, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<CoolifyEnvironment>> RetrieveAllEnvironmentsAsync(
            string projectUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.RetrieveAllEnvironmentsAsync(projectUuid, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<CoolifyEnvironment> AddEnvironmentAsync(
            string projectUuid, CoolifyEnvironment environment, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.AddEnvironmentAsync(projectUuid, environment, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<CoolifyEnvironment> RetrieveEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.projectService.RetrieveEnvironmentAsync(
                    projectUuid, environmentNameOrUuid, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveEnvironmentAsync(
            string projectUuid, string environmentNameOrUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.projectService.RemoveEnvironmentAsync(projectUuid, environmentNameOrUuid, cancellationToken);
            }
            catch (ProjectValidationException projectValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyValidationException projectDependencyValidationException)
            {
                throw CreateProjectClientValidationException(
                    projectDependencyValidationException.InnerException as Xeption);
            }
            catch (ProjectDependencyException projectDependencyException)
            {
                throw CreateProjectClientDependencyException(
                    projectDependencyException.InnerException as Xeption);
            }
            catch (ProjectServiceException projectServiceException)
            {
                throw CreateProjectClientDependencyException(
                    projectServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateProjectClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static ProjectClientValidationException
            CreateProjectClientValidationException(Xeption innerException)
        {
            return new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static ProjectClientDependencyException
            CreateProjectClientDependencyException(Xeption innerException)
        {
            return new ProjectClientDependencyException(
                message: "Project client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static ProjectClientServiceException
            CreateProjectClientServiceException(Xeption innerException)
        {
            return new ProjectClientServiceException(
                message: "Project client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
