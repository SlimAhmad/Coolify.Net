// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Services.Foundations.PrivateKeys;
using Coolify.Resource.Manager.Services.Processings.PrivateKeys;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingServiceTests
    {
        private readonly Mock<IPrivateKeyService> privateKeyServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IPrivateKeyProcessingService privateKeyProcessingService;

        public PrivateKeyProcessingServiceTests()
        {
            this.privateKeyServiceMock = new Mock<IPrivateKeyService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.privateKeyProcessingService = new PrivateKeyProcessingService(
                privateKeyService: this.privateKeyServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static PrivateKey CreateRandomPrivateKey() =>
            new PrivateKey { Uuid = GetRandomString(), Name = GetRandomString() };
    }
}
