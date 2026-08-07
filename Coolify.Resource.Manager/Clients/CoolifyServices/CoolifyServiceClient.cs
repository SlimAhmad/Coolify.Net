// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.CoolifyServices.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions;
using Coolify.Resource.Manager.Services.Processings.CoolifyServices;
using Xeptions;

namespace Coolify.Resource.Manager.Clients.CoolifyServices
{
    /// <summary>Provides one-click service provisioning and management operations.</summary>
    internal class CoolifyServiceClient : ICoolifyServiceClient
    {
        private readonly ICoolifyServiceProcessingService coolifyServiceProcessingService;

        public CoolifyServiceClient(ICoolifyServiceProcessingService coolifyServiceProcessingService) =>
            this.coolifyServiceProcessingService = coolifyServiceProcessingService;

        public async ValueTask<IEnumerable<CoolifyService>> RetrieveAllServicesAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.RetrieveAllServicesAsync(cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<CoolifyService> RetrieveServiceByUuidAsync(
            string serviceUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.RetrieveServiceByUuidAsync(
                    serviceUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<CoolifyService> AddCoolifyServiceAsync(
            CoolifyService service, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.AddCoolifyServiceAsync(service, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<CoolifyService> ModifyCoolifyServiceAsync(
            CoolifyService service, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.ModifyCoolifyServiceAsync(service, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveCoolifyServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.coolifyServiceProcessingService.RemoveCoolifyServiceAsync(serviceUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<EnvironmentVariable>> RetrieveAllServiceEnvVarsAsync(
            string serviceUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.RetrieveAllServiceEnvVarsAsync(
                    serviceUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<EnvironmentVariable> AddServiceEnvVarAsync(
            string serviceUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.AddServiceEnvVarAsync(
                    serviceUuid, environmentVariable, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<EnvironmentVariable> ModifyServiceEnvVarAsync(
            string serviceUuid, EnvironmentVariable environmentVariable, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.ModifyServiceEnvVarAsync(
                    serviceUuid, environmentVariable, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<IEnumerable<EnvironmentVariable>> ModifyBulkServiceEnvVarsAsync(
            string serviceUuid,
            IEnumerable<EnvironmentVariable> environmentVariables,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.coolifyServiceProcessingService.ModifyBulkServiceEnvVarsAsync(
                    serviceUuid, environmentVariables, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemoveServiceEnvVarAsync(
            string serviceUuid, string environmentVariableUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.coolifyServiceProcessingService.RemoveServiceEnvVarAsync(
                    serviceUuid, environmentVariableUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask StartServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.coolifyServiceProcessingService.StartServiceAsync(serviceUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask StopServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.coolifyServiceProcessingService.StopServiceAsync(serviceUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RestartServiceAsync(
            string serviceUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.coolifyServiceProcessingService.RestartServiceAsync(serviceUuid, cancellationToken);
            }
            catch (CoolifyServiceProcessingValidationException coolifyServiceValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyValidationException coolifyServiceDependencyValidationException)
            {
                throw CreateCoolifyServiceClientValidationException(
                    coolifyServiceDependencyValidationException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingDependencyException coolifyServiceDependencyException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceDependencyException.InnerException as Xeption);
            }
            catch (CoolifyServiceProcessingServiceException coolifyServiceServiceException)
            {
                throw CreateCoolifyServiceClientDependencyException(
                    coolifyServiceServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreateCoolifyServiceClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static CoolifyServiceClientValidationException
            CreateCoolifyServiceClientValidationException(Xeption innerException)
        {
            return new CoolifyServiceClientValidationException(
                message: "Service client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static CoolifyServiceClientDependencyException
            CreateCoolifyServiceClientDependencyException(Xeption innerException)
        {
            return new CoolifyServiceClientDependencyException(
                message: "Service client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static CoolifyServiceClientServiceException
            CreateCoolifyServiceClientServiceException(Xeption innerException)
        {
            return new CoolifyServiceClientServiceException(
                message: "Service client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
