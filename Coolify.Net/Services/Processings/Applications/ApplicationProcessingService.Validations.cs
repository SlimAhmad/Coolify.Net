// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Models.Processings.Applications.Exceptions;

namespace Coolify.Net.Services.Processings.Applications
{
    public partial class ApplicationProcessingService
    {
        private static void ValidateApplicationIsNotNull(Application application)
        {
            if (application is null)
            {
                throw new NullApplicationProcessingException(message: "Application is null.");
            }
        }

        private static void ValidateApplicationUuid(string applicationUuid)
        {
            if (string.IsNullOrWhiteSpace(applicationUuid))
            {
                throw new InvalidApplicationProcessingException(message: "Application uuid is invalid.");
            }
        }

        private static void ValidateEnvironmentVariableIsNotNull(EnvironmentVariable environmentVariable)
        {
            if (environmentVariable is null)
            {
                throw new NullApplicationProcessingException(message: "Environment variable is null.");
            }
        }

        private static void ValidateEnvironmentVariablesIsNotNull(IEnumerable<EnvironmentVariable> environmentVariables)
        {
            if (environmentVariables is null)
            {
                throw new NullApplicationProcessingException(message: "Environment variables are null.");
            }
        }

        private static void ValidateEnvironmentVariableUuid(string environmentVariableUuid)
        {
            if (string.IsNullOrWhiteSpace(environmentVariableUuid))
            {
                throw new InvalidApplicationProcessingException(message: "Environment variable uuid is invalid.");
            }
        }
    }
}
