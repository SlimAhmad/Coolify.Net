// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Systems;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Systems
{
    public partial class SystemProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveVersionAsync()
        {
            SystemInfo randomSystemInfo = CreateRandomSystemInfo();

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomSystemInfo);

            SystemInfo actualSystemInfo = await this.systemProcessingService.RetrieveVersionAsync();

            actualSystemInfo.Should().BeEquivalentTo(randomSystemInfo);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCheckHealthAsync()
        {
            this.systemServiceMock
                .Setup(service => service.CheckHealthAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemProcessingService.CheckHealthAsync();

            actualResult.Should().BeTrue();

            this.systemServiceMock.Verify(
                service => service.CheckHealthAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEnableApiAsync()
        {
            this.systemServiceMock
                .Setup(service => service.EnableApiAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemProcessingService.EnableApiAsync();

            actualResult.Should().BeTrue();

            this.systemServiceMock.Verify(
                service => service.EnableApiAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldDisableApiAsync()
        {
            this.systemServiceMock
                .Setup(service => service.DisableApiAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            bool actualResult = await this.systemProcessingService.DisableApiAsync();

            actualResult.Should().BeTrue();

            this.systemServiceMock.Verify(
                service => service.DisableApiAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
