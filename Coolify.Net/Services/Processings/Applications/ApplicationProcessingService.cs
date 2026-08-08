// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Services.Foundations.Applications;

namespace Coolify.Net.Services.Processings.Applications
{
    public partial class ApplicationProcessingService : IApplicationProcessingService
    {
        private readonly IApplicationService applicationService;
        private readonly ILoggingBroker loggingBroker;

        public ApplicationProcessingService(
            IApplicationService applicationService,
            ILoggingBroker loggingBroker)
        {
            this.applicationService = applicationService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Application>> RetrieveAllApplicationsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.applicationService.RetrieveAllApplicationsAsync(cancellationToken);
                });

        public ValueTask<Application> RetrieveApplicationByUuidAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    return await this.applicationService.RetrieveApplicationByUuidAsync(applicationUuid, cancellationToken);
                });

        public ValueTask<Application> AddPublicApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationIsNotNull(application);

                    return await this.applicationService.AddPublicApplicationAsync(application, cancellationToken);
                });

        public ValueTask<Application> AddPrivateGithubAppApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationIsNotNull(application);

                    return await this.applicationService.AddPrivateGithubAppApplicationAsync(
                        application, cancellationToken);
                });

        public ValueTask<Application> AddPrivateDeployKeyApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationIsNotNull(application);

                    return await this.applicationService.AddPrivateDeployKeyApplicationAsync(
                        application, cancellationToken);
                });

        public ValueTask<Application> AddDockerfileApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationIsNotNull(application);

                    return await this.applicationService.AddDockerfileApplicationAsync(application, cancellationToken);
                });

        public ValueTask<Application> AddDockerImageApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationIsNotNull(application);

                    return await this.applicationService.AddDockerImageApplicationAsync(application, cancellationToken);
                });

        public ValueTask<Application> ModifyApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationIsNotNull(application);

                    return await this.applicationService.ModifyApplicationAsync(application, cancellationToken);
                });

        public ValueTask RemoveApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    await this.applicationService.RemoveApplicationAsync(applicationUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllApplicationEnvVarsAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    return await this.applicationService.RetrieveAllApplicationEnvVarsAsync(
                        applicationUuid, cancellationToken);
                });

        public ValueTask<EnvironmentVariable> AddApplicationEnvVarAsync(
            string applicationUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariableIsNotNull(environmentVariable);

                    return await this.applicationService.AddApplicationEnvVarAsync(
                        applicationUuid, environmentVariable, cancellationToken);
                });

        public ValueTask<EnvironmentVariable> ModifyApplicationEnvVarAsync(
            string applicationUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariableIsNotNull(environmentVariable);

                    return await this.applicationService.ModifyApplicationEnvVarAsync(
                        applicationUuid, environmentVariable, cancellationToken);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkApplicationEnvVarsAsync(
            string applicationUuid,
            IEnumerable<EnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariablesIsNotNull(environmentVariables);

                    return await this.applicationService.ModifyBulkApplicationEnvVarsAsync(
                        applicationUuid, environmentVariables, cancellationToken);
                });

        public ValueTask RemoveApplicationEnvVarAsync(
            string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariableUuid(environmentVariableUuid);

                    await this.applicationService.RemoveApplicationEnvVarAsync(
                        applicationUuid, environmentVariableUuid, cancellationToken);
                });

        public ValueTask StartApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    await this.applicationService.StartApplicationAsync(applicationUuid, cancellationToken);
                });

        public ValueTask StopApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    await this.applicationService.StopApplicationAsync(applicationUuid, cancellationToken);
                });

        public ValueTask RestartApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    await this.applicationService.RestartApplicationAsync(applicationUuid, cancellationToken);
                });
    }
}
