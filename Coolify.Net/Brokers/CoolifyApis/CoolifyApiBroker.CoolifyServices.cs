// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.CoolifyServices;
using Coolify.Net.Models.Externals.EnvironmentVariables;

namespace Coolify.Net.Brokers.CoolifyApis
{
    public partial class CoolifyApiBroker
    {
        private const string ServicesRelativeUrl = "services";

        public async ValueTask<IEnumerable<ExternalCoolifyService>> GetAllServicesAsync(
            CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalCoolifyService>>(ServicesRelativeUrl, cancellationToken);

        public async ValueTask<ExternalCoolifyService> GetServiceByUuidAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<ExternalCoolifyService>($"{ServicesRelativeUrl}/{serviceUuid}", cancellationToken);

        public async ValueTask<ExternalCoolifyService> PostServiceAsync(
            ExternalCoolifyService service, CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalCoolifyService>(ServicesRelativeUrl, service, cancellationToken);

        public async ValueTask<ExternalCoolifyService> PatchServiceAsync(
            ExternalCoolifyService service, CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalCoolifyService>(
                    $"{ServicesRelativeUrl}/{service.Uuid}", service, cancellationToken);

        public async ValueTask DeleteServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync($"{ServicesRelativeUrl}/{serviceUuid}", cancellationToken);

        public async ValueTask<IEnumerable<ExternalEnvironmentVariable>> GetServiceEnvVarsAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                await GetAsync<IEnumerable<ExternalEnvironmentVariable>>(
                    $"{ServicesRelativeUrl}/{serviceUuid}/envs", cancellationToken);

        public async ValueTask<ExternalEnvironmentVariable> PostServiceEnvVarAsync(
            string serviceUuid,
            ExternalEnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                await PostAsync<ExternalEnvironmentVariable>(
                    $"{ServicesRelativeUrl}/{serviceUuid}/envs", environmentVariable, cancellationToken);

        public async ValueTask<ExternalEnvironmentVariable> PatchServiceEnvVarAsync(
            string serviceUuid,
            ExternalEnvironmentVariable environmentVariable,
            CancellationToken cancellationToken = default) =>
                await PatchAsync<ExternalEnvironmentVariable>(
                    $"{ServicesRelativeUrl}/{serviceUuid}/envs", environmentVariable, cancellationToken);

        public async ValueTask<IEnumerable<ExternalEnvironmentVariable>> PatchServiceEnvVarsBulkAsync(
            string serviceUuid,
            IEnumerable<ExternalEnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default) =>
                await PatchAsync<IEnumerable<ExternalEnvironmentVariable>>(
                    $"{ServicesRelativeUrl}/{serviceUuid}/envs/bulk", environmentVariables, cancellationToken);

        public async ValueTask DeleteServiceEnvVarAsync(
            string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default) =>
                await DeleteAsync(
                    $"{ServicesRelativeUrl}/{serviceUuid}/envs/{environmentVariableUuid}", cancellationToken);

        public async ValueTask PostServiceStartAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{ServicesRelativeUrl}/{serviceUuid}/start", cancellationToken);

        public async ValueTask PostServiceStopAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{ServicesRelativeUrl}/{serviceUuid}/stop", cancellationToken);

        public async ValueTask PostServiceRestartAsync(
            string serviceUuid, CancellationToken cancellationToken = default) =>
                await PostAsync($"{ServicesRelativeUrl}/{serviceUuid}/restart", cancellationToken);
    }
}
