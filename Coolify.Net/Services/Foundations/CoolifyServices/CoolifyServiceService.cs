// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.CoolifyServices;
using Coolify.Net.Models.Externals.EnvironmentVariables;
using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.EnvironmentVariables;

namespace Coolify.Net.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceService : ICoolifyServiceService
    {
        private readonly ICoolifyApiBroker coolifyApiBroker;
        private readonly ILoggingBroker loggingBroker;

        public CoolifyServiceService(
            ICoolifyApiBroker coolifyApiBroker,
            ILoggingBroker loggingBroker)
        {
            this.coolifyApiBroker = coolifyApiBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<CoolifyService>> RetrieveAllServicesAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    IEnumerable<ExternalCoolifyService> externalCoolifyServices =
                        await this.coolifyApiBroker.GetAllServicesAsync(cancellationToken);

                    return externalCoolifyServices.Select(ConvertToCoolifyService);
                });

        public ValueTask<CoolifyService> RetrieveServiceByUuidAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    ExternalCoolifyService externalCoolifyService =
                        await this.coolifyApiBroker.GetServiceByUuidAsync(serviceUuid, cancellationToken);

                    return ConvertToCoolifyService(externalCoolifyService);
                });

        public ValueTask<CoolifyService> AddCoolifyServiceAsync(
            CoolifyService service, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateCoolifyService(service);

                    ExternalCoolifyService externalCoolifyService = ConvertToExternalCoolifyService(service);

                    ExternalCoolifyService returnedExternalCoolifyService =
                        await this.coolifyApiBroker.PostServiceAsync(externalCoolifyService, cancellationToken);

                    return ConvertToCoolifyService(returnedExternalCoolifyService);
                });

        public ValueTask<CoolifyService> ModifyCoolifyServiceAsync(
            CoolifyService service, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateCoolifyService(service);

                    ExternalCoolifyService externalCoolifyService = ConvertToExternalCoolifyService(service);

                    ExternalCoolifyService returnedExternalCoolifyService =
                        await this.coolifyApiBroker.PatchServiceAsync(externalCoolifyService, cancellationToken);

                    return ConvertToCoolifyService(returnedExternalCoolifyService);
                });

        public ValueTask RemoveCoolifyServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    await this.coolifyApiBroker.DeleteServiceAsync(serviceUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllServiceEnvVarsAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    IEnumerable<ExternalEnvironmentVariable> externalEnvironmentVariables =
                        await this.coolifyApiBroker.GetServiceEnvVarsAsync(serviceUuid, cancellationToken);

                    return externalEnvironmentVariables.Select(ConvertToEnvironmentVariable);
                });

        public ValueTask<EnvironmentVariable> AddServiceEnvVarAsync(
            string serviceUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable externalEnvironmentVariable =
                        ConvertToExternalEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable returnedExternalEnvironmentVariable =
                        await this.coolifyApiBroker.PostServiceEnvVarAsync(
                            serviceUuid, externalEnvironmentVariable, cancellationToken);

                    return ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);
                });

        public ValueTask<EnvironmentVariable> ModifyServiceEnvVarAsync(
            string serviceUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable externalEnvironmentVariable =
                        ConvertToExternalEnvironmentVariable(environmentVariable);

                    ExternalEnvironmentVariable returnedExternalEnvironmentVariable =
                        await this.coolifyApiBroker.PatchServiceEnvVarAsync(
                            serviceUuid, externalEnvironmentVariable, cancellationToken);

                    return ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkServiceEnvVarsAsync(
            string serviceUuid,
            IEnumerable<EnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariables(environmentVariables);

                    IEnumerable<ExternalEnvironmentVariable> externalEnvironmentVariables =
                        environmentVariables.Select(ConvertToExternalEnvironmentVariable);

                    IEnumerable<ExternalEnvironmentVariable> returnedExternalEnvironmentVariables =
                        await this.coolifyApiBroker.PatchServiceEnvVarsBulkAsync(
                            serviceUuid, externalEnvironmentVariables, cancellationToken);

                    return returnedExternalEnvironmentVariables.Select(ConvertToEnvironmentVariable);
                });

        public ValueTask RemoveServiceEnvVarAsync(
            string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariableUuid(environmentVariableUuid);

                    await this.coolifyApiBroker.DeleteServiceEnvVarAsync(
                        serviceUuid, environmentVariableUuid, cancellationToken);
                });

        public ValueTask StartServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    await this.coolifyApiBroker.PostServiceStartAsync(serviceUuid, cancellationToken);
                });

        public ValueTask StopServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    await this.coolifyApiBroker.PostServiceStopAsync(serviceUuid, cancellationToken);
                });

        public ValueTask RestartServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    await this.coolifyApiBroker.PostServiceRestartAsync(serviceUuid, cancellationToken);
                });

        // ---- Conversion helpers ----

        private static CoolifyService ConvertToCoolifyService(ExternalCoolifyService externalCoolifyService) =>
            new CoolifyService
            {
                Uuid = externalCoolifyService.Uuid,
                Name = externalCoolifyService.Name,
                Description = externalCoolifyService.Description,
                DockerComposeRaw = externalCoolifyService.DockerComposeRaw,
                ServiceType = externalCoolifyService.ServiceType,
                ServerUuid = externalCoolifyService.ServerUuid,
                ProjectUuid = externalCoolifyService.ProjectUuid,
                EnvironmentUuid = externalCoolifyService.EnvironmentUuid,
                EnvironmentName = externalCoolifyService.EnvironmentName,
                InstantDeploy = externalCoolifyService.InstantDeploy,
                Status = externalCoolifyService.Status,
                CreatedAt = externalCoolifyService.CreatedAt,
                UpdatedAt = externalCoolifyService.UpdatedAt
            };

        private static ExternalCoolifyService ConvertToExternalCoolifyService(CoolifyService service) =>
            new ExternalCoolifyService
            {
                Uuid = service.Uuid,
                Name = service.Name,
                Description = service.Description,
                DockerComposeRaw = service.DockerComposeRaw,
                ServiceType = service.ServiceType,
                ServerUuid = service.ServerUuid,
                ProjectUuid = service.ProjectUuid,
                EnvironmentUuid = service.EnvironmentUuid,
                EnvironmentName = service.EnvironmentName,
                InstantDeploy = service.InstantDeploy
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
