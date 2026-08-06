// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Applications;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalApplication>> GetAllApplicationsAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> GetApplicationByUuidAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> PostPublicApplicationAsync(ExternalApplication application, CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> PostPrivateGithubAppApplicationAsync(ExternalApplication application, CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> PostPrivateDeployKeyApplicationAsync(ExternalApplication application, CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> PostDockerfileApplicationAsync(ExternalApplication application, CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> PostDockerImageApplicationAsync(ExternalApplication application, CancellationToken cancellationToken = default);
        ValueTask<ExternalApplication> PatchApplicationAsync(ExternalApplication application, CancellationToken cancellationToken = default);
        ValueTask DeleteApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalEnvironmentVariable>> GetApplicationEnvVarsAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalEnvironmentVariable> PostApplicationEnvVarAsync(string applicationUuid, ExternalEnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<ExternalEnvironmentVariable> PatchApplicationEnvVarAsync(string applicationUuid, ExternalEnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalEnvironmentVariable>> PatchApplicationEnvVarsBulkAsync(string applicationUuid, IEnumerable<ExternalEnvironmentVariable> environmentVariables, CancellationToken cancellationToken = default);
        ValueTask DeleteApplicationEnvVarAsync(string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default);
        ValueTask PostApplicationStartAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask PostApplicationStopAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask PostApplicationRestartAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
