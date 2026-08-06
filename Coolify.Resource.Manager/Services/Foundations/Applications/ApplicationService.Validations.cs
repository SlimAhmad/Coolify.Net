// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;

namespace Coolify.Resource.Manager.Services.Foundations.Applications
{
    public partial class ApplicationService
    {
        private void ValidateApplication(Application application)
        {
            ValidateApplicationIsNotNull(application);

            Validate(
                (IsInvalid(application.Name), nameof(Application.Name)),
                (IsInvalid(application.ServerUuid), nameof(Application.ServerUuid)),
                (IsInvalid(application.ProjectUuid), nameof(Application.ProjectUuid)));
        }

        private void ValidateApplicationUuid(string applicationUuid) =>
            Validate((IsInvalid(applicationUuid), nameof(applicationUuid)));

        private void ValidateEnvironmentVariable(EnvironmentVariable environmentVariable)
        {
            ValidateEnvironmentVariableIsNotNull(environmentVariable);

            Validate((IsInvalid(environmentVariable.Key), nameof(EnvironmentVariable.Key)));
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
            Validate((IsInvalid(environmentVariableUuid), nameof(environmentVariableUuid)));

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

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidApplicationException =
                new InvalidApplicationException(
                    message: "Invalid application. Please fix the errors and try again.");

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
