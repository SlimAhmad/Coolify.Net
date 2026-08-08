// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Models.Processings.CoolifyServices.Exceptions;

namespace Coolify.Net.Services.Processings.CoolifyServices
{
    public partial class CoolifyServiceProcessingService
    {
        private static void ValidateCoolifyServiceIsNotNull(CoolifyService service)
        {
            if (service is null)
            {
                throw new NullCoolifyServiceProcessingException(message: "Service is null.");
            }
        }

        private static void ValidateServiceUuid(string serviceUuid)
        {
            if (string.IsNullOrWhiteSpace(serviceUuid))
            {
                throw new InvalidCoolifyServiceProcessingException(message: "Service uuid is invalid.");
            }
        }

        private static void ValidateEnvironmentVariableIsNotNull(EnvironmentVariable environmentVariable)
        {
            if (environmentVariable is null)
            {
                throw new NullCoolifyServiceProcessingException(message: "Environment variable is null.");
            }
        }

        private static void ValidateEnvironmentVariablesIsNotNull(IEnumerable<EnvironmentVariable> environmentVariables)
        {
            if (environmentVariables is null)
            {
                throw new NullCoolifyServiceProcessingException(message: "Environment variables are null.");
            }
        }

        private static void ValidateEnvironmentVariableUuid(string environmentVariableUuid)
        {
            if (string.IsNullOrWhiteSpace(environmentVariableUuid))
            {
                throw new InvalidCoolifyServiceProcessingException(message: "Environment variable uuid is invalid.");
            }
        }
    }
}
