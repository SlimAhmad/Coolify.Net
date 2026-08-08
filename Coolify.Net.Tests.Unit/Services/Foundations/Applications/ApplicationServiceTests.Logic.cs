// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Applications;
using Coolify.Net.Models.Externals.EnvironmentVariables;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllApplicationsAsync()
        {
            List<ExternalApplication> randomExternalApplications =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalApplication()).ToList();

            IEnumerable<Application> expectedApplications = randomExternalApplications.Select(ConvertToApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalApplications);

            IEnumerable<Application> actualApplications =
                await this.applicationService.RetrieveAllApplicationsAsync();

            actualApplications.Should().BeEquivalentTo(expectedApplications);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveApplicationByUuidAsync()
        {
            ExternalApplication randomExternalApplication = CreateRandomExternalApplication();
            string inputApplicationUuid = randomExternalApplication.Uuid;
            Application expectedApplication = ConvertToApplication(randomExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalApplication);

            Application actualApplication =
                await this.applicationService.RetrieveApplicationByUuidAsync(inputApplicationUuid);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPublicApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            ExternalApplication inputExternalApplication = ConvertToExternalApplication(inputApplication);
            ExternalApplication returnedExternalApplication = CreateRandomExternalApplication();
            Application expectedApplication = ConvertToApplication(returnedExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(
                    It.Is<ExternalApplication>(external => IsSameExternalApplication(external, inputExternalApplication)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalApplication);

            Application actualApplication =
                await this.applicationService.AddPublicApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(
                    It.Is<ExternalApplication>(external => IsSameExternalApplication(external, inputExternalApplication)),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPrivateGithubAppApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            ExternalApplication returnedExternalApplication = CreateRandomExternalApplication();
            Application expectedApplication = ConvertToApplication(returnedExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateGithubAppApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalApplication);

            Application actualApplication =
                await this.applicationService.AddPrivateGithubAppApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateGithubAppApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPrivateDeployKeyApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            ExternalApplication returnedExternalApplication = CreateRandomExternalApplication();
            Application expectedApplication = ConvertToApplication(returnedExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPrivateDeployKeyApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalApplication);

            Application actualApplication =
                await this.applicationService.AddPrivateDeployKeyApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPrivateDeployKeyApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddDockerfileApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            ExternalApplication returnedExternalApplication = CreateRandomExternalApplication();
            Application expectedApplication = ConvertToApplication(returnedExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDockerfileApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalApplication);

            Application actualApplication =
                await this.applicationService.AddDockerfileApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDockerfileApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddDockerImageApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            ExternalApplication returnedExternalApplication = CreateRandomExternalApplication();
            Application expectedApplication = ConvertToApplication(returnedExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDockerImageApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalApplication);

            Application actualApplication =
                await this.applicationService.AddDockerImageApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDockerImageApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            ExternalApplication returnedExternalApplication = CreateRandomExternalApplication();
            Application expectedApplication = ConvertToApplication(returnedExternalApplication);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalApplication);

            Application actualApplication =
                await this.applicationService.ModifyApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(expectedApplication);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationService.RemoveApplicationAsync(inputApplicationUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllApplicationEnvVarsAsync()
        {
            string inputApplicationUuid = GetRandomString();

            List<ExternalEnvironmentVariable> randomExternalEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalEnvironmentVariable()).ToList();

            IEnumerable<EnvironmentVariable> expectedEnvironmentVariables =
                randomExternalEnvironmentVariables.Select(ConvertToEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationEnvVarsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomExternalEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.applicationService.RetrieveAllApplicationEnvVarsAsync(inputApplicationUuid);

            actualEnvironmentVariables.Should().BeEquivalentTo(expectedEnvironmentVariables);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationEnvVarsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddApplicationEnvVarAsync()
        {
            string inputApplicationUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            ExternalEnvironmentVariable returnedExternalEnvironmentVariable = CreateRandomExternalEnvironmentVariable();
            EnvironmentVariable expectedEnvironmentVariable = ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationEnvVarAsync(
                    inputApplicationUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.applicationService.AddApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(expectedEnvironmentVariable);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationEnvVarAsync(
                    inputApplicationUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyApplicationEnvVarAsync()
        {
            string inputApplicationUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            ExternalEnvironmentVariable returnedExternalEnvironmentVariable = CreateRandomExternalEnvironmentVariable();
            EnvironmentVariable expectedEnvironmentVariable = ConvertToEnvironmentVariable(returnedExternalEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarAsync(
                    inputApplicationUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.applicationService.ModifyApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(expectedEnvironmentVariable);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarAsync(
                    inputApplicationUuid, It.IsAny<ExternalEnvironmentVariable>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyBulkApplicationEnvVarsAsync()
        {
            string inputApplicationUuid = GetRandomString();

            List<EnvironmentVariable> inputEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable()).ToList();

            List<ExternalEnvironmentVariable> returnedExternalEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalEnvironmentVariable()).ToList();

            IEnumerable<EnvironmentVariable> expectedEnvironmentVariables =
                returnedExternalEnvironmentVariables.Select(ConvertToEnvironmentVariable);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchApplicationEnvVarsBulkAsync(
                    inputApplicationUuid,
                    It.IsAny<IEnumerable<ExternalEnvironmentVariable>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(returnedExternalEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.applicationService.ModifyBulkApplicationEnvVarsAsync(
                    inputApplicationUuid, inputEnvironmentVariables);

            actualEnvironmentVariables.Should().BeEquivalentTo(expectedEnvironmentVariables);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchApplicationEnvVarsBulkAsync(
                    inputApplicationUuid,
                    It.IsAny<IEnumerable<ExternalEnvironmentVariable>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveApplicationEnvVarAsync()
        {
            string inputApplicationUuid = GetRandomString();
            string inputEnvironmentVariableUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteApplicationEnvVarAsync(
                    inputApplicationUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationService.RemoveApplicationEnvVarAsync(
                inputApplicationUuid, inputEnvironmentVariableUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteApplicationEnvVarAsync(
                    inputApplicationUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStartApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationStartAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationService.StartApplicationAsync(inputApplicationUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationStartAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStopApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationStopAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationService.StopApplicationAsync(inputApplicationUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationStopAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRestartApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationRestartAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationService.RestartApplicationAsync(inputApplicationUuid);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationRestartAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
