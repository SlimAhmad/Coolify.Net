// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Deployments.Exceptions;

namespace Coolify.Resource.Manager.Services.Foundations.Deployments
{
    public partial class DeploymentService
    {
        private void ValidateDeploymentUuid(string deploymentUuid) =>
            Validate((IsInvalid(deploymentUuid), nameof(deploymentUuid)));

        private void ValidateApplicationUuid(string applicationUuid) =>
            Validate((IsInvalid(applicationUuid), nameof(applicationUuid)));

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidDeploymentException =
                new InvalidDeploymentException(
                    message: "Invalid deployment. Please fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidDeploymentException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidDeploymentException.ThrowIfContainsErrors();
        }
    }
}
