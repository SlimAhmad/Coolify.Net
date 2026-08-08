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

            Validate((IsInvalid(project.Name), nameof(Project.Name)));
        }

        private void ValidateProjectUuid(string projectUuid) =>
            Validate((IsInvalid(projectUuid), nameof(projectUuid)));

        private void ValidateEnvironment(CoolifyEnvironment environment)
        {
            ValidateEnvironmentIsNotNull(environment);

            Validate((IsInvalid(environment.Name), nameof(CoolifyEnvironment.Name)));
        }

        private void ValidateEnvironmentNameOrUuid(string environmentNameOrUuid) =>
            Validate((IsInvalid(environmentNameOrUuid), nameof(environmentNameOrUuid)));

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

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidProjectException =
                new InvalidProjectException(
                    message: "Invalid project. Please fix the errors and try again.");

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
