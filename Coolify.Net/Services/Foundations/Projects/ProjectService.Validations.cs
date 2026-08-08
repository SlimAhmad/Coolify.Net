// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Foundations.Projects.Exceptions;

namespace Coolify.Net.Services.Foundations.Projects
{
    public partial class ProjectService
    {
        private void ValidateProject(Project project)
        {
            ValidateProjectIsNotNull(project);

            Validate(
                message: "Invalid project. Please fix the errors and try again.",
                (Rule: IsInvalid(project.Name), Parameter: nameof(Project.Name)));
        }

        private void ValidateProjectUuid(string projectUuid) =>
            Validate(
                message: "Invalid project. Please fix the errors and try again.",
                (Rule: IsInvalid(projectUuid), Parameter: nameof(projectUuid)));

        private void ValidateEnvironment(CoolifyEnvironment environment)
        {
            ValidateEnvironmentIsNotNull(environment);

            Validate(
                message: "Invalid project. Please fix the errors and try again.",
                (Rule: IsInvalid(environment.Name), Parameter: nameof(CoolifyEnvironment.Name)));
        }

        private void ValidateEnvironmentNameOrUuid(string environmentNameOrUuid) =>
            Validate(
                message: "Invalid project. Please fix the errors and try again.",
                (Rule: IsInvalid(environmentNameOrUuid), Parameter: nameof(environmentNameOrUuid)));

        private static void ValidateProjectIsNotNull(Project project)
        {
            if (project is null)
            {
                throw new NullProjectException(message: "Project is null.");
            }
        }

        private static void ValidateEnvironmentIsNotNull(CoolifyEnvironment environment)
        {
            if (environment is null)
            {
                throw new NullProjectException(message: "Environment is null.");
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(string message, params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidProjectException = new InvalidProjectException(message);

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidProjectException.UpsertDataList(key: parameter, value: rule.Message);
                }
            }

            invalidProjectException.ThrowIfContainsErrors();
        }
    }
}
