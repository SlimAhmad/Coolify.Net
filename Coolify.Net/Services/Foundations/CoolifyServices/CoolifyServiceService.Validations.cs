// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Coolify.Net.Models.Foundations.EnvironmentVariables;

namespace Coolify.Net.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceService
    {
        private void ValidateCoolifyService(CoolifyService service)
        {
            ValidateCoolifyServiceIsNotNull(service);

            Validate(
                message: "Invalid service. Please fix the errors and try again.",

                (Rule: IsInvalid(service.Name), Parameter: nameof(CoolifyService.Name)),
                (Rule: IsInvalid(service.ServerUuid), Parameter: nameof(CoolifyService.ServerUuid)),
                (Rule: IsInvalid(service.ProjectUuid), Parameter: nameof(CoolifyService.ProjectUuid)));
        }

        private void ValidateServiceUuid(string serviceUuid) =>
            Validate(
                message: "Invalid service. Please fix the errors and try again.",
                (Rule: IsInvalid(serviceUuid), Parameter: nameof(serviceUuid)));

        private void ValidateEnvironmentVariable(EnvironmentVariable environmentVariable)
        {
            ValidateEnvironmentVariableIsNotNull(environmentVariable);

            Validate(
                message: "Invalid service. Please fix the errors and try again.",
                (Rule: IsInvalid(environmentVariable.Key), Parameter: nameof(EnvironmentVariable.Key)));
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
            Validate(
                message: "Invalid service. Please fix the errors and try again.",
                (Rule: IsInvalid(environmentVariableUuid), Parameter: nameof(environmentVariableUuid)));

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

        private static void Validate(string message, params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidCoolifyServiceException = new InvalidCoolifyServiceException(message);

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
