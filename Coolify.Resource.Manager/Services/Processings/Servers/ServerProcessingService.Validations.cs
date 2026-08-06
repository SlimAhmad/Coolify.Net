// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Processings.Servers.Exceptions;

namespace Coolify.Resource.Manager.Services.Processings.Servers
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
