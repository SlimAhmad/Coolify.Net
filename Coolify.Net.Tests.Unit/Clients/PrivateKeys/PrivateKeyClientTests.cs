// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Models.Processings.PrivateKeys.Exceptions;
using Coolify.Net.Services.Processings.PrivateKeys;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.PrivateKeys
{
    public partial class PrivateKeyClientTests
    {
        private readonly Mock<IPrivateKeyProcessingService> privateKeyServiceMock;
        private readonly IPrivateKeyClient privateKeyClient;

        public PrivateKeyClientTests()
        {
            this.privateKeyServiceMock = new Mock<IPrivateKeyProcessingService>();

            this.privateKeyClient = new PrivateKeyClient(
                privateKeyProcessingService: this.privateKeyServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static PrivateKey CreateRandomPrivateKey() =>
            new PrivateKey { Uuid = GetRandomString(), Name = GetRandomString() };

        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new PrivateKeyProcessingValidationException("test", inner),
                new PrivateKeyProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new PrivateKeyProcessingDependencyException("test", inner),
                new PrivateKeyProcessingServiceException("test", inner)
            };
        }
    }
}
