// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Clients.Systems
{
    public partial class SystemClientTests
    {
        [Fact]
        public async Task ShouldRetrieveVersionAsync()
        {
            SystemInfo randomSystemInfo = CreateRandomSystemInfo();

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomSystemInfo);

            SystemInfo actualSystemInfo = await this.systemClient.RetrieveVersionAsync();

            actualSystemInfo.Should().BeEquivalentTo(randomSystemInfo);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCheckHealthAsync()
        {
            this.systemServiceMock
                .Setup(service => service.CheckHealthAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemClient.CheckHealthAsync();

            actualResult.Should().BeTrue();

            this.systemServiceMock.Verify(
                service => service.CheckHealthAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEnableApiAsync()
        {
            this.systemServiceMock
                .Setup(service => service.EnableApiAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemClient.EnableApiAsync();

            actualResult.Should().BeTrue();

            this.systemServiceMock.Verify(
                service => service.EnableApiAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDisableApiAsync()
        {
            this.systemServiceMock
                .Setup(service => service.DisableApiAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemClient.DisableApiAsync();

            actualResult.Should().BeTrue();

            this.systemServiceMock.Verify(
                service => service.DisableApiAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }
    }
}
