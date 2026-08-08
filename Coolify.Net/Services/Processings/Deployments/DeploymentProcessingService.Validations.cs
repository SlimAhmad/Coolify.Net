// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Processings.Deployments.Exceptions;

namespace Coolify.Net.Services.Processings.Deployments
{
    public partial class DeploymentProcessingService
    {
        private static void ValidateDeploymentUuid(string deploymentUuid)
        {
            if (string.IsNullOrWhiteSpace(deploymentUuid))
            {
                throw new InvalidDeploymentProcessingException(message: "Deployment uuid is invalid.");
            }
        }

        private static void ValidateApplicationUuid(string applicationUuid)
        {
            if (string.IsNullOrWhiteSpace(applicationUuid))
            {
                throw new InvalidDeploymentProcessingException(message: "Application uuid is invalid.");
            }
        }
    }
}
