// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Servers.Exceptions;
using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Processings.Servers.Exceptions;
using Coolify.Net.Services.Processings.Servers;
using Xeptions;

namespace Coolify.Net.Clients.Servers
{
    /// <summary>Provides server provisioning and management operations.</summary>
    internal class ServerClient : IServerClient
    {
        private readonly IServerProcessingService serverProcessingService;

        public ServerClient(IServerProcessingService serverProcessingService) =>
            this.serverProcessingService = serverProcessingService;

        public async ValueTask<IEnumerable<Server>> RetrieveAllServersAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.RetrieveAllServersAsync(cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Server> RetrieveServerByUuidAsync(
            string serverUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.RetrieveServerByUuidAsync(serverUuid, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Server> AddServerAsync(Server server, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.AddServerAsync(server, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Server> ModifyServerAsync(Server server, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.ModifyServerAsync(server, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveServerAsync(string serverUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.serverProcessingService.RemoveServerAsync(serverUuid, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<Server> RetrieveServerValidationAsync(
            string serverUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.RetrieveServerValidationAsync(serverUuid, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<object>> RetrieveServerResourcesAsync(
            string serverUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.RetrieveServerResourcesAsync(serverUuid, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<string>> RetrieveServerDomainsAsync(
            string serverUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverProcessingService.RetrieveServerDomainsAsync(serverUuid, cancellationToken);
            }
            catch (ServerProcessingValidationException serverProcessingValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyValidationException serverProcessingDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverProcessingDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerProcessingDependencyException serverProcessingDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingDependencyException.InnerException as Xeption);
            }
            catch (ServerProcessingServiceException serverProcessingServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverProcessingServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateServerClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static ServerClientValidationException
            CreateServerClientValidationException(Xeption innerException)
        {
            return new ServerClientValidationException(
                message: "Server client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static ServerClientDependencyException
            CreateServerClientDependencyException(Xeption innerException)
        {
            return new ServerClientDependencyException(
                message: "Server client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static ServerClientServiceException
            CreateServerClientServiceException(Xeption innerException)
        {
            return new ServerClientServiceException(
                message: "Server client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
