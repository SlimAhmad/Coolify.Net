// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Clients.CoolifyServices
{
    public partial class CoolifyServiceClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllServicesAsync()
        {
            IEnumerable<CoolifyService> randomCoolifyServices =
                Enumerable.Range(0, 3).Select(_ => CreateRandomCoolifyService());

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomCoolifyServices);

            IEnumerable<CoolifyService> actualCoolifyServices = await this.coolifyServiceClient.RetrieveAllServicesAsync();

            actualCoolifyServices.Should().BeEquivalentTo(randomCoolifyServices);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveServiceByUuidAsync()
        {
            CoolifyService randomCoolifyService = CreateRandomCoolifyService();
            string inputServiceUuid = randomCoolifyService.Uuid;

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveServiceByUuidAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomCoolifyService);

            CoolifyService actualCoolifyService =
                await this.coolifyServiceClient.RetrieveServiceByUuidAsync(inputServiceUuid);

            actualCoolifyService.Should().BeEquivalentTo(randomCoolifyService);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RetrieveServiceByUuidAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddCoolifyServiceAsync()
        {
            CoolifyService inputCoolifyService = CreateRandomCoolifyService();
            CoolifyService randomCoolifyService = CreateRandomCoolifyService();

            this.coolifyServiceServiceMock
                .Setup(service => service.AddCoolifyServiceAsync(inputCoolifyService, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomCoolifyService);

            CoolifyService actualCoolifyService = await this.coolifyServiceClient.AddCoolifyServiceAsync(inputCoolifyService);

            actualCoolifyService.Should().BeEquivalentTo(randomCoolifyService);

            this.coolifyServiceServiceMock.Verify(service =>
                service.AddCoolifyServiceAsync(inputCoolifyService, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyCoolifyServiceAsync()
        {
            CoolifyService inputCoolifyService = CreateRandomCoolifyService();
            CoolifyService randomCoolifyService = CreateRandomCoolifyService();

            this.coolifyServiceServiceMock
                .Setup(service => service.ModifyCoolifyServiceAsync(inputCoolifyService, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomCoolifyService);

            CoolifyService actualCoolifyService = await this.coolifyServiceClient.ModifyCoolifyServiceAsync(inputCoolifyService);

            actualCoolifyService.Should().BeEquivalentTo(randomCoolifyService);

            this.coolifyServiceServiceMock.Verify(service =>
                service.ModifyCoolifyServiceAsync(inputCoolifyService, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveCoolifyServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyServiceServiceMock
                .Setup(service => service.RemoveCoolifyServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceClient.RemoveCoolifyServiceAsync(inputServiceUuid);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RemoveCoolifyServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllServiceEnvVarsAsync()
        {
            string inputServiceUuid = GetRandomString();
            IEnumerable<EnvironmentVariable> randomEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable());

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServiceEnvVarsAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.coolifyServiceClient.RetrieveAllServiceEnvVarsAsync(inputServiceUuid);

            actualEnvironmentVariables.Should().BeEquivalentTo(randomEnvironmentVariables);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RetrieveAllServiceEnvVarsAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddServiceEnvVarAsync()
        {
            string inputServiceUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            EnvironmentVariable randomEnvironmentVariable = CreateRandomEnvironmentVariable();

            this.coolifyServiceServiceMock
                .Setup(service => service.AddServiceEnvVarAsync(
                    inputServiceUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.coolifyServiceClient.AddServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(randomEnvironmentVariable);

            this.coolifyServiceServiceMock.Verify(service =>
                service.AddServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyServiceEnvVarAsync()
        {
            string inputServiceUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            EnvironmentVariable randomEnvironmentVariable = CreateRandomEnvironmentVariable();

            this.coolifyServiceServiceMock
                .Setup(service => service.ModifyServiceEnvVarAsync(
                    inputServiceUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.coolifyServiceClient.ModifyServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(randomEnvironmentVariable);

            this.coolifyServiceServiceMock.Verify(service =>
                service.ModifyServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyBulkServiceEnvVarsAsync()
        {
            string inputServiceUuid = GetRandomString();

            IEnumerable<EnvironmentVariable> inputEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable());

            IEnumerable<EnvironmentVariable> randomEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable());

            this.coolifyServiceServiceMock
                .Setup(service => service.ModifyBulkServiceEnvVarsAsync(
                    inputServiceUuid, inputEnvironmentVariables, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.coolifyServiceClient.ModifyBulkServiceEnvVarsAsync(
                    inputServiceUuid, inputEnvironmentVariables);

            actualEnvironmentVariables.Should().BeEquivalentTo(randomEnvironmentVariables);

            this.coolifyServiceServiceMock.Verify(service =>
                service.ModifyBulkServiceEnvVarsAsync(
                    inputServiceUuid, inputEnvironmentVariables, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveServiceEnvVarAsync()
        {
            string inputServiceUuid = GetRandomString();
            string inputEnvironmentVariableUuid = GetRandomString();

            this.coolifyServiceServiceMock
                .Setup(service => service.RemoveServiceEnvVarAsync(
                    inputServiceUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceClient.RemoveServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariableUuid);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RemoveServiceEnvVarAsync(
                    inputServiceUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStartServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyServiceServiceMock
                .Setup(service => service.StartServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceClient.StartServiceAsync(inputServiceUuid);

            this.coolifyServiceServiceMock.Verify(service =>
                service.StartServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStopServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyServiceServiceMock
                .Setup(service => service.StopServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceClient.StopServiceAsync(inputServiceUuid);

            this.coolifyServiceServiceMock.Verify(service =>
                service.StopServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRestartServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyServiceServiceMock
                .Setup(service => service.RestartServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceClient.RestartServiceAsync(inputServiceUuid);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RestartServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
