// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Processings.Projects.Exceptions;

namespace Coolify.Net.Services.Processings.Projects
{
    public partial class ProjectProcessingService
    {
        private static void ValidateProjectIsNotNull(Project project)
        {
            if (project is null)
            {
                throw new NullProjectProcessingException(message: "Project is null.");
            }
        }

        private static void ValidateProjectUuid(string projectUuid)
        {
            if (string.IsNullOrWhiteSpace(projectUuid))
            {
                throw new InvalidProjectProcessingException(message: "Project uuid is invalid.");
            }
        }

        private static void ValidateEnvironmentIsNotNull(CoolifyEnvironment environment)
        {
            if (environment is null)
            {
                throw new NullProjectProcessingException(message: "Environment is null.");
            }
        }

        private static void ValidateEnvironmentNameOrUuid(string environmentNameOrUuid)
        {
            if (string.IsNullOrWhiteSpace(environmentNameOrUuid))
            {
                throw new InvalidProjectProcessingException(message: "Environment name or uuid is invalid.");
            }
        }
    }
}
