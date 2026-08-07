// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.CoolifyServices;
using Coolify.Resource.Manager.Models.Externals.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllServicesAsync()
        {
            List<ExternalCoolifyService> randomExternalCoolifyServices =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalCoolifyService()).ToList();

            IEnumerable<CoolifyService> expectedCoolifyServices =
                randomExternalCoolifyServices.Select(ConvertToCoolifyService);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalCoolifyServices);

            IEnumerable<CoolifyService> actualCoolifyServices =
                await this.coolifyServiceService.RetrieveAllServicesAsync();

            actualCoolifyServices.Should().BeEquivalentTo(expectedCoolifyServices);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveServiceByUuidAsync()
        {
            ExternalCoolifyService randomExternalCoolifyService = CreateRandomExternalCoolifyService();
            string inputServiceUuid = randomExternalCoolifyService.Uuid;
            CoolifyService expectedCoolifyService = ConvertToCoolifyService(randomExternalCoolifyService);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalCoolifyService);

            CoolifyService actualCoolifyService =
                await this.coolifyServiceService.RetrieveServiceByUuidAsync(inputServiceUuid);

            actualCoolifyService.Should().BeEquivalentTo(expectedCoolifyService);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddCoolifyServiceAsync()
        {
            CoolifyService inputCoolifyService = CreateRandomCoolifyService();
            ExternalCoolifyService inputExternalCoolifyService = ConvertToExternalCoolifyService(inputCoolifyService);
            ExternalCoolifyService returnedExternalCoolifyService = CreateRandomExternalCoolifyService();
            CoolifyService expectedCoolifyService = ConvertToCoolifyService(returnedExternalCoolifyService);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceAsync(
                    It.Is<ExternalCoolifyService>(external => IsSameExternalCoolifyService(external, inputExternalCoolifyService)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalCoolifyService);

            CoolifyService actualCoolifyService =
                await this.coolifyServiceService.AddCoolifyServiceAsync(inputCoolifyService);

            actualCoolifyService.Should().BeEquivalentTo(expectedCoolifyService);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceAsync(
                    It.Is<ExternalCoolifyService>(external => IsSameExternalCoolifyService(external, inputExternalCoolifyService)),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyCoolifyServiceAsync()
        {
            CoolifyService inputCoolifyService = CreateRandomCoolifyService();
            ExternalCoolifyService returnedExternalCoolifyService = CreateRandomExternalCoolifyService();
            CoolifyService expectedCoolifyService = ConvertToCoolifyService(returnedExternalCoolifyService);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalCoolifyService);

            CoolifyService actualCoolifyService =
                await this.coolifyServiceService.ModifyCoolifyServiceAsync(inputCoolifyService);

            actualCoolifyService.Should().BeEquivalentTo(expectedCoolifyService);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveCoolifyServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceService.RemoveCoolifyServiceAsync(inputServiceUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServiceAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllServiceEnvVarsAsync()
        {
            string inputServiceUuid = GetRandomString();

            List<ExternalEnvironmentVariable> randomExternalEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalEnvironmentVariable()).ToList();

            IEnumerable<EnvironmentVariable> expectedEnvironmentVariables =
                randomExternalEnvironmentVariables.Select(ConvertToEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceEnvVarsAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.coolifyServiceService.RetrieveAllServiceEnvVarsAsync(inputServiceUuid);

            actualEnvironmentVariables.Should().BeEquivalentTo(expectedEnvironmentVariables);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceEnvVarsAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddServiceEnvVarAsync()
        {
            string inputServiceUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            ExternalEnvironmentVariable returnedExternalEnvironmentVariable = CreateRandomExternalEnvironmentVariable();
            EnvironmentVariable expectedEnvironmentVariable = ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceEnvVarAsync(
                    inputServiceUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.coolifyServiceService.AddServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(expectedEnvironmentVariable);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceEnvVarAsync(
                    inputServiceUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyServiceEnvVarAsync()
        {
            string inputServiceUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            ExternalEnvironmentVariable returnedExternalEnvironmentVariable = CreateRandomExternalEnvironmentVariable();
            EnvironmentVariable expectedEnvironmentVariable = ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarAsync(
                    inputServiceUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.coolifyServiceService.ModifyServiceEnvVarAsync(inputServiceUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(expectedEnvironmentVariable);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarAsync(
                    inputServiceUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyBulkServiceEnvVarsAsync()
        {
            string inputServiceUuid = GetRandomString();

            List<EnvironmentVariable> inputEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable()).ToList();

            List<ExternalEnvironmentVariable> returnedExternalEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalEnvironmentVariable()).ToList();

            IEnumerable<EnvironmentVariable> expectedEnvironmentVariables =
                returnedExternalEnvironmentVariables.Select(ConvertToEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceEnvVarsBulkAsync(
                    inputServiceUuid,
                    It.IsAny<IEnumerable<ExternalEnvironmentVariable>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.coolifyServiceService.ModifyBulkServiceEnvVarsAsync(
                    inputServiceUuid, inputEnvironmentVariables);

            actualEnvironmentVariables.Should().BeEquivalentTo(expectedEnvironmentVariables);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceEnvVarsBulkAsync(
                    inputServiceUuid,
                    It.IsAny<IEnumerable<ExternalEnvironmentVariable>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveServiceEnvVarAsync()
        {
            string inputServiceUuid = GetRandomString();
            string inputEnvironmentVariableUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServiceEnvVarAsync(
                    inputServiceUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceService.RemoveServiceEnvVarAsync(
                inputServiceUuid, inputEnvironmentVariableUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServiceEnvVarAsync(
                    inputServiceUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStartServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceStartAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceService.StartServiceAsync(inputServiceUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceStartAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStopServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceStopAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceService.StopServiceAsync(inputServiceUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceStopAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRestartServiceAsync()
        {
            string inputServiceUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceRestartAsync(inputServiceUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.coolifyServiceService.RestartServiceAsync(inputServiceUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceRestartAsync(inputServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
