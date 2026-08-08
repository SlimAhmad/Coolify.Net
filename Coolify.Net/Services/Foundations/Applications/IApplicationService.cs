// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.EnvironmentVariables;

namespace Coolify.Net.Services.Foundations.Applications
{
    public interface IApplicationService
    {
        ValueTask<IEnumerable<Application>> RetrieveAllApplicationsAsync(CancellationToken cancellationToken = default);
        ValueTask<Application> RetrieveApplicationByUuidAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask<Application> AddPublicApplicationAsync(Application application, CancellationToken cancellationToken = default);
        ValueTask<Application> AddPrivateGithubAppApplicationAsync(Application application, CancellationToken cancellationToken = default);
        ValueTask<Application> AddPrivateDeployKeyApplicationAsync(Application application, CancellationToken cancellationToken = default);
        ValueTask<Application> AddDockerfileApplicationAsync(Application application, CancellationToken cancellationToken = default);
        ValueTask<Application> AddDockerImageApplicationAsync(Application application, CancellationToken cancellationToken = default);
        ValueTask<Application> ModifyApplicationAsync(Application application, CancellationToken cancellationToken = default);
        ValueTask RemoveApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllApplicationEnvVarsAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask<EnvironmentVariable> AddApplicationEnvVarAsync(string applicationUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<EnvironmentVariable> ModifyApplicationEnvVarAsync(string applicationUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkApplicationEnvVarsAsync(string applicationUuid, IEnumerable<EnvironmentVariable> environmentVariables, CancellationToken cancellationToken = default);
        ValueTask RemoveApplicationEnvVarAsync(string applicationUuid, string environmentVariableUuid, CancellationToken cancellationToken = default);
        ValueTask StartApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask StopApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);
        ValueTask RestartApplicationAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
