// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Servers.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using Coolify.Resource.Manager.Services.Foundations.Servers;
using Xeptions;

namespace Coolify.Resource.Manager.Clients.Servers
{
    /// <summary>Provides server provisioning and management operations.</summary>
    internal class ServerClient : IServerClient
    {
        private readonly IServerService serverService;

        public ServerClient(IServerService serverService) =>
            this.serverService = serverService;

        public async ValueTask<IEnumerable<Server>> RetrieveAllServersAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.serverService.RetrieveAllServersAsync(cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                return await this.serverService.RetrieveServerByUuidAsync(serverUuid, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                return await this.serverService.AddServerAsync(server, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                return await this.serverService.ModifyServerAsync(server, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                await this.serverService.RemoveServerAsync(serverUuid, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                return await this.serverService.RetrieveServerValidationAsync(serverUuid, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                return await this.serverService.RetrieveServerResourcesAsync(serverUuid, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
                return await this.serverService.RetrieveServerDomainsAsync(serverUuid, cancellationToken);
            }
            catch (ServerValidationException serverValidationException)
            {
                throw CreateServerClientValidationException(
                    serverValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyValidationException serverDependencyValidationException)
            {
                throw CreateServerClientValidationException(
                    serverDependencyValidationException.InnerException as Xeption);
            }
            catch (ServerDependencyException serverDependencyException)
            {
                throw CreateServerClientDependencyException(
                    serverDependencyException.InnerException as Xeption);
            }
            catch (ServerServiceException serverServiceException)
            {
                throw CreateServerClientDependencyException(
                    serverServiceException.InnerException as Xeption);
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
