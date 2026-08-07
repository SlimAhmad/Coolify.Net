// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Services.Foundations.CoolifyServices;

namespace Coolify.Resource.Manager.Services.Processings.CoolifyServices
{
    public partial class CoolifyServiceProcessingService : ICoolifyServiceProcessingService
    {
        private readonly ICoolifyServiceService coolifyServiceService;
        private readonly ILoggingBroker loggingBroker;

        public CoolifyServiceProcessingService(
            ICoolifyServiceService coolifyServiceService,
            ILoggingBroker loggingBroker)
        {
            this.coolifyServiceService = coolifyServiceService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<IEnumerable<CoolifyService>> RetrieveAllServicesAsync(
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    return await this.coolifyServiceService.RetrieveAllServicesAsync(cancellationToken);
                });

        public ValueTask<CoolifyService> RetrieveServiceByUuidAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    return await this.coolifyServiceService.RetrieveServiceByUuidAsync(serviceUuid, cancellationToken);
                });

        public ValueTask<CoolifyService> AddCoolifyServiceAsync(
            CoolifyService service, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateCoolifyServiceIsNotNull(service);

                    return await this.coolifyServiceService.AddCoolifyServiceAsync(service, cancellationToken);
                });

        public ValueTask<CoolifyService> ModifyCoolifyServiceAsync(
            CoolifyService service, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateCoolifyServiceIsNotNull(service);

                    return await this.coolifyServiceService.ModifyCoolifyServiceAsync(service, cancellationToken);
                });

        public ValueTask RemoveCoolifyServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    await this.coolifyServiceService.RemoveCoolifyServiceAsync(serviceUuid, cancellationToken);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllServiceEnvVarsAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    return await this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(
                        serviceUuid, cancellationToken);
                });

        public ValueTask<EnvironmentVariable> AddServiceEnvVarAsync(
            string serviceUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariableIsNotNull(environmentVariable);

                    return await this.coolifyServiceService.AddServiceEnvVarAsync(
                        serviceUuid, environmentVariable, cancellationToken);
                });

        public ValueTask<EnvironmentVariable> ModifyServiceEnvVarAsync(
            string serviceUuid,
            EnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariableIsNotNull(environmentVariable);

                    return await this.coolifyServiceService.ModifyServiceEnvVarAsync(
                        serviceUuid, environmentVariable, cancellationToken);
                });

        public ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkServiceEnvVarsAsync(
            string serviceUuid,
            IEnumerable<EnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariablesIsNotNull(environmentVariables);

                    return await this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(
                        serviceUuid, environmentVariables, cancellationToken);
                });

        public ValueTask RemoveServiceEnvVarAsync(
            string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);
                    ValidateEnvironmentVariableUuid(environmentVariableUuid);

                    await this.coolifyServiceService.RemoveServiceEnvVarAsync(
                        serviceUuid, environmentVariableUuid, cancellationToken);
                });

        public ValueTask StartServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    await this.coolifyServiceService.StartServiceAsync(serviceUuid, cancellationToken);
                });

        public ValueTask StopServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    await this.coolifyServiceService.StopServiceAsync(serviceUuid, cancellationToken);
                });

        public ValueTask RestartServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                TryCatch(async () =>
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    ValidateServiceUuid(serviceUuid);

                    await this.coolifyServiceService.RestartServiceAsync(serviceUuid, cancellationToken);
                });
    }
}
