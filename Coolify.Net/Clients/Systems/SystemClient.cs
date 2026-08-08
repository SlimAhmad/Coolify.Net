// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Systems.Exceptions;
using Coolify.Net.Models.Foundations.Systems;
using Coolify.Net.Models.Processings.Systems.Exceptions;
using Coolify.Net.Services.Processings.Systems;
using Xeptions;

namespace Coolify.Net.Clients.Systems
{
    /// <summary>Provides read access to Coolify instance/system status.</summary>
    internal class SystemClient : ISystemClient
    {
        private readonly ISystemProcessingService systemProcessingService;

        public SystemClient(ISystemProcessingService systemProcessingService) =>
            this.systemProcessingService = systemProcessingService;

        public async ValueTask<SystemInfo> RetrieveVersionAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.systemProcessingService.RetrieveVersionAsync(cancellationToken);
            }
            catch (SystemProcessingValidationException systemValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyValidationException systemDependencyValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemDependencyValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyException systemDependencyException)
            {
                throw CreateSystemClientDependencyException(
                    systemDependencyException.InnerException as Xeption);
            }
            catch (SystemProcessingServiceException systemServiceException)
            {
                throw CreateSystemClientDependencyException(
                    systemServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateSystemClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<bool> CheckHealthAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.systemProcessingService.CheckHealthAsync(cancellationToken);
            }
            catch (SystemProcessingValidationException systemValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyValidationException systemDependencyValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemDependencyValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyException systemDependencyException)
            {
                throw CreateSystemClientDependencyException(
                    systemDependencyException.InnerException as Xeption);
            }
            catch (SystemProcessingServiceException systemServiceException)
            {
                throw CreateSystemClientDependencyException(
                    systemServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateSystemClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<bool> EnableApiAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.systemProcessingService.EnableApiAsync(cancellationToken);
            }
            catch (SystemProcessingValidationException systemValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyValidationException systemDependencyValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemDependencyValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyException systemDependencyException)
            {
                throw CreateSystemClientDependencyException(
                    systemDependencyException.InnerException as Xeption);
            }
            catch (SystemProcessingServiceException systemServiceException)
            {
                throw CreateSystemClientDependencyException(
                    systemServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateSystemClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<bool> DisableApiAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.systemProcessingService.DisableApiAsync(cancellationToken);
            }
            catch (SystemProcessingValidationException systemValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyValidationException systemDependencyValidationException)
            {
                throw CreateSystemClientValidationException(
                    systemDependencyValidationException.InnerException as Xeption);
            }
            catch (SystemProcessingDependencyException systemDependencyException)
            {
                throw CreateSystemClientDependencyException(
                    systemDependencyException.InnerException as Xeption);
            }
            catch (SystemProcessingServiceException systemServiceException)
            {
                throw CreateSystemClientDependencyException(
                    systemServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateSystemClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static SystemClientValidationException
            CreateSystemClientValidationException(Xeption innerException)
        {
            return new SystemClientValidationException(
                message: "System client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static SystemClientDependencyException
            CreateSystemClientDependencyException(Xeption innerException)
        {
            return new SystemClientDependencyException(
                message: "System client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static SystemClientServiceException
            CreateSystemClientServiceException(Xeption innerException)
        {
            return new SystemClientServiceException(
                message: "System client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
