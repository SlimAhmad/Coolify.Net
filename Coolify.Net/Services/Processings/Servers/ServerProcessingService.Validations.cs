// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Processings.Servers.Exceptions;

namespace Coolify.Net.Services.Processings.Servers
{
    public partial class ServerProcessingService
    {
        private static void ValidateServerIsNotNull(Server server)
        {
            if (server is null)
            {
                throw new NullServerProcessingException(message: "Server is null.");
            }
        }

        private static void ValidateServerUuid(string serverUuid)
        {
            if (string.IsNullOrWhiteSpace(serverUuid))
            {
                throw new InvalidServerProcessingException(message: "Server uuid is invalid.");
            }
        }
    }
}
