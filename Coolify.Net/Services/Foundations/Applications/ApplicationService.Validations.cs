// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Coolify.Net.Models.Foundations.EnvironmentVariables;

namespace Coolify.Net.Services.Foundations.Applications
{
    public partial class ApplicationService
    {
        private void ValidateApplication(Application application)
        {
            ValidateApplicationIsNotNull(application);

            Validate(
                message: "Invalid application. Please fix the errors and try again.",

                (Rule: IsInvalid(application.Name), Parameter: nameof(Application.Name)),
                (Rule: IsInvalid(application.ServerUuid), Parameter: nameof(Application.ServerUuid)),
                (Rule: IsInvalid(application.ProjectUuid), Parameter: nameof(Application.ProjectUuid)));
        }

        private void ValidateApplicationUuid(string applicationUuid) =>
            Validate(
                message: "Invalid application. Please fix the errors and try again.",
                (Rule: IsInvalid(applicationUuid), Parameter: nameof(applicationUuid)));

        private void ValidateEnvironmentVariable(EnvironmentVariable environmentVariable)
        {
            ValidateEnvironmentVariableIsNotNull(environmentVariable);

            Validate(
                message: "Invalid application. Please fix the errors and try again.",
                (Rule: IsInvalid(environmentVariable.Key), Parameter: nameof(EnvironmentVariable.Key)));
        }

        private void ValidateEnvironmentVariables(IEnumerable<EnvironmentVariable> environmentVariables)
        {
            if (environmentVariables is null)
            {
                throw new NullApplicationException(message: "Environment variables are null.");
            }

            foreach (EnvironmentVariable environmentVariable in environmentVariables)
            {
                ValidateEnvironmentVariable(environmentVariable);
            }
        }

        private void ValidateEnvironmentVariableUuid(string environmentVariableUuid) =>
            Validate(
                message: "Invalid application. Please fix the errors and try again.",
                (Rule: IsInvalid(environmentVariableUuid), Parameter: nameof(environmentVariableUuid)));

        private static void ValidateApplicationIsNotNull(Application application)
        {
            if (application is null)
            {
                throw new NullApplicationException(message: "Application is null.");
            }
        }

        private static void ValidateEnvironmentVariableIsNotNull(EnvironmentVariable environmentVariable)
        {
            if (environmentVariable is null)
            {
                throw new NullApplicationException(message: "Environment variable is null.");
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(string message, params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidApplicationException = new InvalidApplicationException(message);

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidApplicationException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidApplicationException.ThrowIfContainsErrors();
        }
    }
}
