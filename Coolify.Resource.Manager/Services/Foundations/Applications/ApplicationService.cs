// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.CoolifyApis;
using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Externals.Applications;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;

namespace Coolify.Resource.Manager.Services.Foundations.Applications
{
    public partial class ApplicationService : IApplicationService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public ApplicationService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<Application>> RetrieveAllApplicationsAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalApplication> externalApplications =
                        await this.coolifyApiBroker.GetAllApplicationsAsync(cancellationToken);

                    return externalApplications.Select(ConvertToApplication);
                });

        public ValueTask<Application> RetrieveApplicationByUuidAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    ExternalApplication externalApplication =
                        await this.coolifyApiBroker.GetApplicationByUuidAsync(applicationUuid, cancellationToken);

                    return ConvertToApplication(externalApplication);
                });

        public ValueTask<Application> AddPublicApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplication(application);

                    ExternalApplication externalApplication = ConvertToExternalApplication(application);

                    ExternalApplication returnedExternalApplication =
                        await this.coolifyApiBroker.PostPublicApplicationAsync(externalApplication, cancellationToken);

                    return ConvertToApplication(returnedExternalApplication);
                });

        public ValueTask<Application> AddPrivateGithubAppApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplication(application);

                    ExternalApplication externalApplication = ConvertToExternalApplication(application);

                    ExternalApplication returnedExternalApplication =
                        await this.coolifyApiBroker.PostPrivateGithubAppApplicationAsync(
                            externalApplication, cancellationToken);

                    return ConvertToApplication(returnedExternalApplication);
                });

        public ValueTask<Application> AddPrivateDeployKeyApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplication(application);

                    ExternalApplication externalApplication = ConvertToExternalApplication(application);

                    ExternalApplication returnedExternalApplication =
                        await this.coolifyApiBroker.PostPrivateDeployKeyApplicationAsync(
                            externalApplication, cancellationToken);

                    return ConvertToApplication(returnedExternalApplication);
                });

        public ValueTask<Application> AddDockerfileApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplication(application);

                    ExternalApplication externalApplication = ConvertToExternalApplication(application);

                    ExternalApplication returnedExternalApplication =
                        await this.coolifyApiBroker.PostDockerfileApplicationAsync(
                            externalApplication, cancellationToken);

                    return ConvertToApplication(returnedExternalApplication);
                });

        public ValueTask<Application> AddDockerImageApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplication(application);

                    ExternalApplication externalApplication = ConvertToExternalApplication(application);

                    ExternalApplication returnedExternalApplication =
                        await this.coolifyApiBroker.PostDockerImageApplicationAsync(
                            externalApplication, cancellationToken);

                    return ConvertToApplication(returnedExternalApplication);
                });

        public ValueTask<Application> ModifyApplicationAsync(
            Application application, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplication(application);

                    ExternalApplication externalApplication = ConvertToExternalApplication(application);

                    ExternalApplication returnedExternalApplication =
                        await this.coolifyApiBroker.PatchApplicationAsync(externalApplication, cancellationToken);

                    return ConvertToApplication(returnedExternalApplication);
                });

        public ValueTask RemoveApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    await this.coolifyApiBroker.DeleteApplicationAsync(applicationUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllApplicationEnvVarsAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);

                    IEnumerable<ExternalEnvironmentVariable> externalEnvironmentVariables =
                        await this.coolifyApiBroker.GetApplicationEnvVarsAsync(applicationUuid, cancellationToken);

                    return externalEnvironmentVariables.Select(ConvertToEnvironmentVariable);
                });

        public ValueTask<EnvironmentVariable> AddApplicationEnvVarAsync(
            string applicationUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable externalEnvironmentVariable =
                        ConvertToExternalEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable returnedExternalEnvironmentVariable =
                        await this.coolifyApiBroker.PostApplicationEnvVarAsync(
                            applicationUuid, externalEnvironmentVariable, cancellationToken);

                    return ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);
                });

        public ValueTask<EnvironmentVariable> ModifyApplicationEnvVarAsync(
            string applicationUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable externalEnvironmentVariable =
                        ConvertToExternalEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable returnedExternalEnvironmentVariable =
                        await this.coolifyApiBroker.PatchApplicationEnvVarAsync(
                            applicationUuid, externalEnvironmentVariable, cancellationToken);

                    return ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkApplicationEnvVarsAsync(
            string applicationUuid,
            IEnumerable<EnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariables(environmentVariables);

                    IEnumerable<ExternalEnvironmentVariable> externalEnvironmentVariables =
                        environmentVariables.Select(ConvertToExternalEnvironmentVariable);

                    IEnumerable<ExternalEnvironmentVariable> returnedExternalEnvironmentVariables =
                        await this.coolifyApiBroker.PatchApplicationEnvVarsBulkAsync(
                            applicationUuid, externalEnvironmentVariables, cancellationToken);

                    return returnedExternalEnvironmentVariables.Select(ConvertToEnvironmentVariable);
                });

        public ValueTask RemoveApplicationEnvVarAsync(
            string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    ValidateEnvironmentVariableUuid(environmentVariableUuid);

                    await this.coolifyApiBroker.DeleteApplicationEnvVarAsync(
                        applicationUuid, environmentVariableUuid, cancellationToken);
                });

        public ValueTask StartApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    await this.coolifyApiBroker.PostApplicationStartAsync(applicationUuid, cancellationToken);
                });

        public ValueTask StopApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    await this.coolifyApiBroker.PostApplicationStopAsync(applicationUuid, cancellationToken);
                });

        public ValueTask RestartApplicationAsync(
            string applicationUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateApplicationUuid(applicationUuid);
                    await this.coolifyApiBroker.PostApplicationRestartAsync(applicationUuid, cancellationToken);
                });

        // ---- Conversion helpers ----

        private static Application ConvertToApplication(ExternalApplication externalApplication) =>
            new Application
            {
                Uuid = externalApplication.Uuid,
                Name = externalApplication.Name,
                Description = externalApplication.Description,
                Fqdn = externalApplication.Fqdn,
                GitRepository = externalApplication.GitRepository,
                GitBranch = externalApplication.GitBranch,
                GitCommitSha = externalApplication.GitCommitSha,
                BuildPack = externalApplication.BuildPack,
                DockerfileLocation = externalApplication.DockerfileLocation,
                DockerComposeLocation = externalApplication.DockerComposeLocation,
                DockerComposeRaw = externalApplication.DockerComposeRaw,
                DockerImage = externalApplication.DockerImage,
                ServerUuid = externalApplication.ServerUuid,
                ProjectUuid = externalApplication.ProjectUuid,
                EnvironmentUuid = externalApplication.EnvironmentUuid,
                EnvironmentName = externalApplication.EnvironmentName,
                Status = externalApplication.Status,
                InstantDeploy = externalApplication.InstantDeploy,
                AutoDeployEnabled = externalApplication.AutoDeployEnabled,
                CreatedAt = externalApplication.CreatedAt,
                UpdatedAt = externalApplication.UpdatedAt
            };

        private static ExternalApplication ConvertToExternalApplication(Application application) =>
            new ExternalApplication
            {
                Uuid = application.Uuid,
                Name = application.Name,
                Description = application.Description,
                Fqdn = application.Fqdn,
                GitRepository = application.GitRepository,
                GitBranch = application.GitBranch,
                GitCommitSha = application.GitCommitSha,
                BuildPack = application.BuildPack,
                DockerfileLocation = application.DockerfileLocation,
                DockerComposeLocation = application.DockerComposeLocation,
                DockerComposeRaw = application.DockerComposeRaw,
                DockerImage = application.DockerImage,
                ServerUuid = application.ServerUuid,
                ProjectUuid = application.ProjectUuid,
                EnvironmentUuid = application.EnvironmentUuid,
                EnvironmentName = application.EnvironmentName,
                InstantDeploy = application.InstantDeploy,
                AutoDeployEnabled = application.AutoDeployEnabled
            };

        private static EnvironmentVariable ConvertToEnvironmentVariable(
            ExternalEnvironmentVariable externalEnvironmentVariable) =>
                new EnvironmentVariable
                {
                    Uuid = externalEnvironmentVariable.Uuid,
                    Key = externalEnvironmentVariable.Key,
                    Value = externalEnvironmentVariable.Value,
                    IsPreview = externalEnvironmentVariable.IsPreview,
                    IsBuildTime = externalEnvironmentVariable.IsBuildTime,
                    IsLiteral = externalEnvironmentVariable.IsLiteral,
                    IsMultiline = externalEnvironmentVariable.IsMultiline,
                    IsShownOnce = externalEnvironmentVariable.IsShownOnce,
                    CreatedAt = externalEnvironmentVariable.CreatedAt,
                    UpdatedAt = externalEnvironmentVariable.UpdatedAt
                };

        private static ExternalEnvironmentVariable ConvertToExternalEnvironmentVariable(
            EnvironmentVariable environmentVariable) =>
                new ExternalEnvironmentVariable
                {
                    Uuid = environmentVariable.Uuid,
                    Key = environmentVariable.Key,
                    Value = environmentVariable.Value,
                    IsPreview = environmentVariable.IsPreview,
                    IsBuildTime = environmentVariable.IsBuildTime,
                    IsLiteral = environmentVariable.IsLiteral,
                    IsMultiline = environmentVariable.IsMultiline,
                    IsShownOnce = environmentVariable.IsShownOnce
                };
    }
}
