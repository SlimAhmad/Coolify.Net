// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Applications.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Services.Foundations.Applications;
using Xeptions;

namespace Coolify.Resource.Manager.Clients.Applications
{
    /// <summary>Provides application provisioning and management operations.</summary>
    internal class ApplicationClient : IApplicationClient
    {
        private readonly IApplicationService applicationService;

        public ApplicationClient(IApplicationService applicationService) =>
            this.applicationService = applicationService;

        public async ValueTask<IEnumerable<Application>> RetrieveAllApplicationsAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.RetrieveAllApplicationsAsync(cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> RetrieveApplicationByUuidAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.RetrieveApplicationByUuidAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> AddPublicApplicationAsync(
            Application application, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.AddPublicApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> AddPrivateGithubAppApplicationAsync(
            Application application, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.AddPrivateGithubAppApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> AddPrivateDeployKeyApplicationAsync(
            Application application, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.AddPrivateDeployKeyApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> AddDockerfileApplicationAsync(
            Application application, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.AddDockerfileApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> AddDockerImageApplicationAsync(
            Application application, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.AddDockerImageApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Application> ModifyApplicationAsync(
            Application application, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.ModifyApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.applicationService.RemoveApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllApplicationEnvVarsAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.RetrieveAllApplicationEnvVarsAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<EnvironmentVariable> AddApplicationEnvVarAsync(
            string applicationUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.AddApplicationEnvVarAsync(
                    applicationUuid, environmentVariable, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<EnvironmentVariable> ModifyApplicationEnvVarAsync(
            string applicationUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.ModifyApplicationEnvVarAsync(
                    applicationUuid, environmentVariable, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkApplicationEnvVarsAsync(
            string applicationUuid,
            IEnumerable<EnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationService.ModifyBulkApplicationEnvVarsAsync(
                    applicationUuid, environmentVariables, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveApplicationEnvVarAsync(
            string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.applicationService.RemoveApplicationEnvVarAsync(
                    applicationUuid, environmentVariableUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask StartApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.applicationService.StartApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask StopApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.applicationService.StopApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RestartApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.applicationService.RestartApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationServiceException applicationServiceException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateApplicationClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static ApplicationClientValidationException
            CreateApplicationClientValidationException(Xeption innerException)
        {
            return new ApplicationClientValidationException(
                message: "Application client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static ApplicationClientDependencyException
            CreateApplicationClientDependencyException(Xeption innerException)
        {
            return new ApplicationClientDependencyException(
                message: "Application client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static ApplicationClientServiceException
            CreateApplicationClientServiceException(Xeption innerException)
        {
            return new ApplicationClientServiceException(
                message: "Application client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
