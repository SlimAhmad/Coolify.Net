// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;

namespace Coolify.Resource.Manager.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceService
    {
        private void ValidateCoolifyService(CoolifyService service)
        {
            ValidateCoolifyServiceIsNotNull(service);

            Validate(
                (IsInvalid(service.Name), nameof(CoolifyService.Name)),
                (IsInvalid(service.ServerUuid), nameof(CoolifyService.ServerUuid)),
                (IsInvalid(service.ProjectUuid), nameof(CoolifyService.ProjectUuid)));
        }

        private void ValidateServiceUuid(string serviceUuid) =>
            Validate((IsInvalid(serviceUuid), nameof(serviceUuid)));

        private void ValidateEnvironmentVariable(EnvironmentVariable environmentVariable)
        {
            ValidateEnvironmentVariableIsNotNull(environmentVariable);

            Validate((IsInvalid(environmentVariable.Key), nameof(EnvironmentVariable.Key)));
        }

        private void ValidateEnvironmentVariables(IEnumerable<EnvironmentVariable> environmentVariables)
        {
            if (environmentVariables is null)
            {
                throw new NullCoolifyServiceException(message: "Environment variables are null.");
            }

            foreach (EnvironmentVariable environmentVariable in environmentVariables)
            {
                ValidateEnvironmentVariable(environmentVariable);
            }
        }

        private void ValidateEnvironmentVariableUuid(string environmentVariableUuid) =>
            Validate((IsInvalid(environmentVariableUuid), nameof(environmentVariableUuid)));

        private static void ValidateCoolifyServiceIsNotNull(CoolifyService service)
        {
            if (service is null)
            {
                throw new NullCoolifyServiceException(message: "Service is null.");
            }
        }

        private static void ValidateEnvironmentVariableIsNotNull(EnvironmentVariable environmentVariable)
        {
            if (environmentVariable is null)
            {
                throw new NullCoolifyServiceException(message: "Environment variable is null.");
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCoolifyServiceException =
                new InvalidCoolifyServiceException(
                    message: "Invalid service. Please fix the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidCoolifyServiceException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidCoolifyServiceException.ThrowIfContainsErrors();
        }
    }
}
