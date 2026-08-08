// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Systems;
using Coolify.Net.Models.Foundations.Systems;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Systems
{
    public partial class SystemServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveVersionAsync()
        {
            ExternalSystemInfo randomExternalSystemInfo = CreateRandomExternalSystemInfo();
            SystemInfo expectedSystemInfo = ConvertToSystemInfo(randomExternalSystemInfo);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalSystemInfo);

            SystemInfo actualSystemInfo = await this.systemService.RetrieveVersionAsync();

            actualSystemInfo.Should().BeEquivalentTo(expectedSystemInfo);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCheckHealthAsync()
        {
            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetHealthCheckAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemService.CheckHealthAsync();

            actualResult.Should().BeTrue();

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetHealthCheckAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEnableApiAsync()
        {
            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnableApiAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemService.EnableApiAsync();

            actualResult.Should().BeTrue();

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetEnableApiAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDisableApiAsync()
        {
            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDisableApiAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemService.DisableApiAsync();

            actualResult.Should().BeTrue();

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetDisableApiAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
