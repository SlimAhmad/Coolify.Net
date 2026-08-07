// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.PrivateKeys.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Models.Processings.PrivateKeys.Exceptions;
using Coolify.Resource.Manager.Services.Processings.PrivateKeys;
using Xeptions;

namespace Coolify.Resource.Manager.Clients.PrivateKeys
{
    /// <summary>Provides private key provisioning and management operations.</summary>
    internal class PrivateKeyClient : IPrivateKeyClient
    {
        private readonly IPrivateKeyProcessingService privateKeyProcessingService;

        public PrivateKeyClient(IPrivateKeyProcessingService privateKeyProcessingService) =>
            this.privateKeyProcessingService = privateKeyProcessingService;

        public async ValueTask<IEnumerable<PrivateKey>> RetrieveAllPrivateKeysAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync(cancellationToken);
            }
            catch (PrivateKeyProcessingValidationException privateKeyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyValidationException privateKeyDependencyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyDependencyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyException privateKeyDependencyException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyDependencyException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingServiceException privateKeyServiceException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreatePrivateKeyClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<PrivateKey> RetrievePrivateKeyByUuidAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.privateKeyProcessingService.RetrievePrivateKeyByUuidAsync(
                    privateKeyUuid, cancellationToken);
            }
            catch (PrivateKeyProcessingValidationException privateKeyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyValidationException privateKeyDependencyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyDependencyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyException privateKeyDependencyException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyDependencyException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingServiceException privateKeyServiceException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreatePrivateKeyClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<PrivateKey> AddPrivateKeyAsync(
            PrivateKey privateKey, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.privateKeyProcessingService.AddPrivateKeyAsync(privateKey, cancellationToken);
            }
            catch (PrivateKeyProcessingValidationException privateKeyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyValidationException privateKeyDependencyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyDependencyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyException privateKeyDependencyException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyDependencyException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingServiceException privateKeyServiceException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreatePrivateKeyClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask<PrivateKey> ModifyPrivateKeyAsync(
            PrivateKey privateKey, CancellationToken cancellationToken = default)
        {
            try
            {
                return await this.privateKeyProcessingService.ModifyPrivateKeyAsync(privateKey, cancellationToken);
            }
            catch (PrivateKeyProcessingValidationException privateKeyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyValidationException privateKeyDependencyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyDependencyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyException privateKeyDependencyException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyDependencyException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingServiceException privateKeyServiceException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreatePrivateKeyClientServiceException(exception as Xeption);
            }
        }

        public async ValueTask RemovePrivateKeyAsync(
            string privateKeyUuid, CancellationToken cancellationToken = default)
        {
            try
            {
                await this.privateKeyProcessingService.RemovePrivateKeyAsync(privateKeyUuid, cancellationToken);
            }
            catch (PrivateKeyProcessingValidationException privateKeyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyValidationException privateKeyDependencyValidationException)
            {
                throw CreatePrivateKeyClientValidationException(
                    privateKeyDependencyValidationException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingDependencyException privateKeyDependencyException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyDependencyException.InnerException as Xeption);
            }
            catch (PrivateKeyProcessingServiceException privateKeyServiceException)
            {
                throw CreatePrivateKeyClientDependencyException(
                    privateKeyServiceException.InnerException as Xeption);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw CreatePrivateKeyClientServiceException(exception as Xeption);
            }
        }

        // ---- Private factory helpers ----

        private static PrivateKeyClientValidationException
            CreatePrivateKeyClientValidationException(Xeption innerException)
        {
            return new PrivateKeyClientValidationException(
                message: "Private key client validation error occurred, fix the errors and try again.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static PrivateKeyClientDependencyException
            CreatePrivateKeyClientDependencyException(Xeption innerException)
        {
            return new PrivateKeyClientDependencyException(
                message: "Private key client dependency error occurred, contact support.",
                innerException: innerException,
                data: innerException.Data);
        }

        private static PrivateKeyClientServiceException
            CreatePrivateKeyClientServiceException(Xeption innerException)
        {
            return new PrivateKeyClientServiceException(
                message: "Private key client service error occurred, contact support.",
                innerException: innerException,
                data: innerException?.Data);
        }
    }
}
