// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.PrivateKeys
{
    public partial class PrivateKeyProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPrivateKeysAsync()
        {
            IEnumerable<PrivateKey> randomPrivateKeys = Enumerable.Range(0, 3).Select(_ => CreateRandomPrivateKey());

            this.privateKeyServiceMock
                .Setup(service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomPrivateKeys);

            IEnumerable<PrivateKey> actualPrivateKeys =
                await this.privateKeyProcessingService.RetrieveAllPrivateKeysAsync();

            actualPrivateKeys.Should().BeEquivalentTo(randomPrivateKeys);

            this.privateKeyServiceMock.Verify(
                service => service.RetrieveAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrievePrivateKeyByUuidAsync()
        {
            PrivateKey randomPrivateKey = CreateRandomPrivateKey();
            string inputPrivateKeyUuid = randomPrivateKey.Uuid;

            this.privateKeyServiceMock
                .Setup(service => service.RetrievePrivateKeyByUuidAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomPrivateKey);

            PrivateKey actualPrivateKey =
                await this.privateKeyProcessingService.RetrievePrivateKeyByUuidAsync(inputPrivateKeyUuid);

            actualPrivateKey.Should().BeEquivalentTo(randomPrivateKey);

            this.privateKeyServiceMock.Verify(service =>
                service.RetrievePrivateKeyByUuidAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPrivateKeyAsync()
        {
            PrivateKey inputPrivateKey = CreateRandomPrivateKey();
            PrivateKey randomPrivateKey = CreateRandomPrivateKey();

            this.privateKeyServiceMock
                .Setup(service => service.AddPrivateKeyAsync(inputPrivateKey, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomPrivateKey);

            PrivateKey actualPrivateKey = await this.privateKeyProcessingService.AddPrivateKeyAsync(inputPrivateKey);

            actualPrivateKey.Should().BeEquivalentTo(randomPrivateKey);

            this.privateKeyServiceMock.Verify(service =>
                service.AddPrivateKeyAsync(inputPrivateKey, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyPrivateKeyAsync()
        {
            PrivateKey inputPrivateKey = CreateRandomPrivateKey();
            PrivateKey randomPrivateKey = CreateRandomPrivateKey();

            this.privateKeyServiceMock
                .Setup(service => service.ModifyPrivateKeyAsync(inputPrivateKey, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomPrivateKey);

            PrivateKey actualPrivateKey = await this.privateKeyProcessingService.ModifyPrivateKeyAsync(inputPrivateKey);

            actualPrivateKey.Should().BeEquivalentTo(randomPrivateKey);

            this.privateKeyServiceMock.Verify(service =>
                service.ModifyPrivateKeyAsync(inputPrivateKey, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemovePrivateKeyAsync()
        {
            string inputPrivateKeyUuid = GetRandomString();

            this.privateKeyServiceMock
                .Setup(service => service.RemovePrivateKeyAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.privateKeyProcessingService.RemovePrivateKeyAsync(inputPrivateKeyUuid);

            this.privateKeyServiceMock.Verify(service =>
                service.RemovePrivateKeyAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.privateKeyServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
