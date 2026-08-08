// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Servers
{
    public partial class ServerProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllServersAsync()
        {
            IEnumerable<Server> randomServers = Enumerable.Range(0, 3).Select(_ => CreateRandomServer());

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomServers);

            IEnumerable<Server> actualServers = await this.serverProcessingService.RetrieveAllServersAsync();

            actualServers.Should().BeEquivalentTo(randomServers);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveServerByUuidAsync()
        {
            Server randomServer = CreateRandomServer();
            string inputServerUuid = randomServer.Uuid;

            this.serverServiceMock
                .Setup(service => service.RetrieveServerByUuidAsync(inputServerUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomServer);

            Server actualServer = await this.serverProcessingService.RetrieveServerByUuidAsync(inputServerUuid);

            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(service =>
                service.RetrieveServerByUuidAsync(inputServerUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddServerAsync()
        {
            Server inputServer = CreateRandomServer();
            Server randomServer = CreateRandomServer();

            this.serverServiceMock
                .Setup(service => service.AddServerAsync(inputServer, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomServer);

            Server actualServer = await this.serverProcessingService.AddServerAsync(inputServer);

            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(
                service => service.AddServerAsync(inputServer, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyServerAsync()
        {
            Server inputServer = CreateRandomServer();
            Server randomServer = CreateRandomServer();

            this.serverServiceMock
                .Setup(service => service.ModifyServerAsync(inputServer, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomServer);

            Server actualServer = await this.serverProcessingService.ModifyServerAsync(inputServer);

            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(
                service => service.ModifyServerAsync(inputServer, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveServerAsync()
        {
            string inputServerUuid = GetRandomString();

            this.serverServiceMock
                .Setup(service => service.RemoveServerAsync(inputServerUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.serverProcessingService.RemoveServerAsync(inputServerUuid);

            this.serverServiceMock.Verify(
                service => service.RemoveServerAsync(inputServerUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveServerValidationAsync()
        {
            Server randomServer = CreateRandomServer();
            string inputServerUuid = randomServer.Uuid;

            this.serverServiceMock
                .Setup(service => service.RetrieveServerValidationAsync(inputServerUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomServer);

            Server actualServer = await this.serverProcessingService.RetrieveServerValidationAsync(inputServerUuid);

            actualServer.Should().BeEquivalentTo(randomServer);

            this.serverServiceMock.Verify(service =>
                service.RetrieveServerValidationAsync(inputServerUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveServerResourcesAsync()
        {
            string inputServerUuid = GetRandomString();
            IEnumerable<object> randomResources = new List<object> { new { Id = GetRandomString() } };

            this.serverServiceMock
                .Setup(service => service.RetrieveServerResourcesAsync(inputServerUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomResources);

            IEnumerable<object> actualResources =
                await this.serverProcessingService.RetrieveServerResourcesAsync(inputServerUuid);

            actualResources.Should().BeEquivalentTo(randomResources);

            this.serverServiceMock.Verify(service =>
                service.RetrieveServerResourcesAsync(inputServerUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveServerDomainsAsync()
        {
            string inputServerUuid = GetRandomString();
            IEnumerable<string> randomDomains = new List<string> { GetRandomString() };

            this.serverServiceMock
                .Setup(service => service.RetrieveServerDomainsAsync(inputServerUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomDomains);

            IEnumerable<string> actualDomains =
                await this.serverProcessingService.RetrieveServerDomainsAsync(inputServerUuid);

            actualDomains.Should().BeEquivalentTo(randomDomains);

            this.serverServiceMock.Verify(service =>
                service.RetrieveServerDomainsAsync(inputServerUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
