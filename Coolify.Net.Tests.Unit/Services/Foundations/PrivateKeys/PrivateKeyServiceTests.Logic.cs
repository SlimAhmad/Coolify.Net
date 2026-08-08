// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.PrivateKeys;
using Coolify.Net.Models.Foundations.PrivateKeys;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.PrivateKeys
{
    public partial class PrivateKeyServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllPrivateKeysAsync()
        {
            List<ExternalPrivateKey> randomExternalPrivateKeys =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalPrivateKey()).ToList();

            IEnumerable<PrivateKey> expectedPrivateKeys = randomExternalPrivateKeys.Select(ConvertToPrivateKey);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalPrivateKeys);

            IEnumerable<PrivateKey> actualPrivateKeys = await this.privateKeyService.RetrieveAllPrivateKeysAsync();

            actualPrivateKeys.Should().BeEquivalentTo(expectedPrivateKeys);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllPrivateKeysAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrievePrivateKeyByUuidAsync()
        {
            ExternalPrivateKey randomExternalPrivateKey = CreateRandomExternalPrivateKey();
            string inputPrivateKeyUuid = randomExternalPrivateKey.Uuid;
            PrivateKey expectedPrivateKey = ConvertToPrivateKey(randomExternalPrivateKey);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetPrivateKeyByUuidAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalPrivateKey);

            PrivateKey actualPrivateKey =
                await this.privateKeyService.RetrievePrivateKeyByUuidAsync(inputPrivateKeyUuid);

            actualPrivateKey.Should().BeEquivalentTo(expectedPrivateKey);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetPrivateKeyByUuidAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPrivateKeyAsync()
        {
            PrivateKey inputPrivateKey = CreateRandomPrivateKey();
            ExternalPrivateKey inputExternalPrivateKey = ConvertToExternalPrivateKey(inputPrivateKey);
            ExternalPrivateKey returnedExternalPrivateKey = CreateRandomExternalPrivateKey();
            PrivateKey expectedPrivateKey = ConvertToPrivateKey(returnedExternalPrivateKey);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateKeyAsync(
                    It.Is<ExternalPrivateKey>(external => IsSameExternalPrivateKey(external, inputExternalPrivateKey)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalPrivateKey);

            PrivateKey actualPrivateKey = await this.privateKeyService.AddPrivateKeyAsync(inputPrivateKey);

            actualPrivateKey.Should().BeEquivalentTo(expectedPrivateKey);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateKeyAsync(
                    It.Is<ExternalPrivateKey>(external => IsSameExternalPrivateKey(external, inputExternalPrivateKey)),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyPrivateKeyAsync()
        {
            PrivateKey inputPrivateKey = CreateRandomPrivateKey();
            ExternalPrivateKey returnedExternalPrivateKey = CreateRandomExternalPrivateKey();
            PrivateKey expectedPrivateKey = ConvertToPrivateKey(returnedExternalPrivateKey);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchPrivateKeyAsync(
                    It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalPrivateKey);

            PrivateKey actualPrivateKey = await this.privateKeyService.ModifyPrivateKeyAsync(inputPrivateKey);

            actualPrivateKey.Should().BeEquivalentTo(expectedPrivateKey);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchPrivateKeyAsync(It.IsAny<ExternalPrivateKey>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemovePrivateKeyAsync()
        {
            string inputPrivateKeyUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeletePrivateKeyAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.privateKeyService.RemovePrivateKeyAsync(inputPrivateKeyUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeletePrivateKeyAsync(inputPrivateKeyUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
