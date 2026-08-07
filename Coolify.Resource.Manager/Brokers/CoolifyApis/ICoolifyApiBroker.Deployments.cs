// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Deployments;

namespace Coolify.Resource.Manager.Brokers.CoolifyApis
{
    public partial interface ICoolifyApiBroker
    {
        ValueTask<IEnumerable<ExternalDeployment>> GetAllDeploymentsAsync(CancellationToken cancellationToken = default);
        ValueTask<ExternalDeployment> GetDeploymentByUuidAsync(string deploymentUuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalDeployment> PostDeployAsync(string uuid, CancellationToken cancellationToken = default);
        ValueTask<ExternalDeployment> PostDeploymentCancelAsync(string deploymentUuid, CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<ExternalDeployment>> GetApplicationDeploymentsAsync(string applicationUuid, CancellationToken cancellationToken = default);
    }
}
