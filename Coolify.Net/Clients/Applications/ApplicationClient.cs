// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Applications.Exceptions;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Models.Processings.Applications.Exceptions;
using Coolify.Net.Services.Processings.Applications;
using Xeptions;

namespace Coolify.Net.Clients.Applications
{
    /// <summary>Provides application provisioning and management operations.</summary>
    internal class ApplicationClient : IApplicationClient
    {
        private readonly IApplicationProcessingService applicationProcessingService;

        public ApplicationClient(IApplicationProcessingService applicationProcessingService) =>
            this.applicationProcessingService = applicationProcessingService;

        public async ValueTask<IEnumerable<Application>> RetrieveAllApplicationsAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.applicationProcessingService.RetrieveAllApplicationsAsync(cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.RetrieveApplicationByUuidAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.AddPublicApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.AddPrivateGithubAppApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.AddPrivateDeployKeyApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.AddDockerfileApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.AddDockerImageApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.ModifyApplicationAsync(application, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                await this.applicationProcessingService.RemoveApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.RetrieveAllApplicationEnvVarsAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.AddApplicationEnvVarAsync(
                    applicationUuid, environmentVariable, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.ModifyApplicationEnvVarAsync(
                    applicationUuid, environmentVariable, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                return await this.applicationProcessingService.ModifyBulkApplicationEnvVarsAsync(
                    applicationUuid, environmentVariables, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                await this.applicationProcessingService.RemoveApplicationEnvVarAsync(
                    applicationUuid, environmentVariableUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                await this.applicationProcessingService.StartApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                await this.applicationProcessingService.StopApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
                await this.applicationProcessingService.RestartApplicationAsync(applicationUuid, cancellationToken);
            }
            catch (ApplicationProcessingValidationException applicationValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyValidationException applicationDependencyValidationException)
            {
                throw CreateApplicationClientValidationException(
                    applicationDependencyValidationException.InnerException as Xeption);
            }
            catch (ApplicationProcessingDependencyException applicationDependencyException)
            {
                throw CreateApplicationClientDependencyException(
                    applicationDependencyException.InnerException as Xeption);
            }
            catch (ApplicationProcessingServiceException applicationServiceException)
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
