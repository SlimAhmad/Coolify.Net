// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Applications
{
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllApplicationsAsync()
        {
            IEnumerable<Application> randomApplications =
                Enumerable.Range(0, 3).Select(_ => CreateRandomApplication());

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplications);

            IEnumerable<Application> actualApplications = await this.applicationClient.RetrieveAllApplicationsAsync();

            actualApplications.Should().BeEquivalentTo(randomApplications);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveApplicationByUuidAsync()
        {
            Application randomApplication = CreateRandomApplication();
            string inputApplicationUuid = randomApplication.Uuid;

            this.applicationServiceMock
                .Setup(service => service.RetrieveApplicationByUuidAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication =
                await this.applicationClient.RetrieveApplicationByUuidAsync(inputApplicationUuid);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.RetrieveApplicationByUuidAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPublicApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            Application randomApplication = CreateRandomApplication();

            this.applicationServiceMock
                .Setup(service => service.AddPublicApplicationAsync(inputApplication, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication = await this.applicationClient.AddPublicApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.AddPublicApplicationAsync(inputApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPrivateGithubAppApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            Application randomApplication = CreateRandomApplication();

            this.applicationServiceMock
                .Setup(service => service.AddPrivateGithubAppApplicationAsync(inputApplication, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication =
                await this.applicationClient.AddPrivateGithubAppApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.AddPrivateGithubAppApplicationAsync(inputApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddPrivateDeployKeyApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            Application randomApplication = CreateRandomApplication();

            this.applicationServiceMock
                .Setup(service => service.AddPrivateDeployKeyApplicationAsync(inputApplication, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication =
                await this.applicationClient.AddPrivateDeployKeyApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.AddPrivateDeployKeyApplicationAsync(inputApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddDockerfileApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            Application randomApplication = CreateRandomApplication();

            this.applicationServiceMock
                .Setup(service => service.AddDockerfileApplicationAsync(inputApplication, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication =
                await this.applicationClient.AddDockerfileApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.AddDockerfileApplicationAsync(inputApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddDockerImageApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            Application randomApplication = CreateRandomApplication();

            this.applicationServiceMock
                .Setup(service => service.AddDockerImageApplicationAsync(inputApplication, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication =
                await this.applicationClient.AddDockerImageApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.AddDockerImageApplicationAsync(inputApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyApplicationAsync()
        {
            Application inputApplication = CreateRandomApplication();
            Application randomApplication = CreateRandomApplication();

            this.applicationServiceMock
                .Setup(service => service.ModifyApplicationAsync(inputApplication, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomApplication);

            Application actualApplication = await this.applicationClient.ModifyApplicationAsync(inputApplication);

            actualApplication.Should().BeEquivalentTo(randomApplication);

            this.applicationServiceMock.Verify(service =>
                service.ModifyApplicationAsync(inputApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.applicationServiceMock
                .Setup(service => service.RemoveApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationClient.RemoveApplicationAsync(inputApplicationUuid);

            this.applicationServiceMock.Verify(service =>
                service.RemoveApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllApplicationEnvVarsAsync()
        {
            string inputApplicationUuid = GetRandomString();
            IEnumerable<EnvironmentVariable> randomEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable());

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationEnvVarsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.applicationClient.RetrieveAllApplicationEnvVarsAsync(inputApplicationUuid);

            actualEnvironmentVariables.Should().BeEquivalentTo(randomEnvironmentVariables);

            this.applicationServiceMock.Verify(service =>
                service.RetrieveAllApplicationEnvVarsAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddApplicationEnvVarAsync()
        {
            string inputApplicationUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            EnvironmentVariable randomEnvironmentVariable = CreateRandomEnvironmentVariable();

            this.applicationServiceMock
                .Setup(service => service.AddApplicationEnvVarAsync(
                    inputApplicationUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.applicationClient.AddApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(randomEnvironmentVariable);

            this.applicationServiceMock.Verify(service =>
                service.AddApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()),
                Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyApplicationEnvVarAsync()
        {
            string inputApplicationUuid = GetRandomString();
            EnvironmentVariable inputEnvironmentVariable = CreateRandomEnvironmentVariable();
            EnvironmentVariable randomEnvironmentVariable = CreateRandomEnvironmentVariable();

            this.applicationServiceMock
                .Setup(service => service.ModifyApplicationEnvVarAsync(
                    inputApplicationUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariable);

            EnvironmentVariable actualEnvironmentVariable =
                await this.applicationClient.ModifyApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariable);

            actualEnvironmentVariable.Should().BeEquivalentTo(randomEnvironmentVariable);

            this.applicationServiceMock.Verify(service =>
                service.ModifyApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariable, It.IsAny<CancellationToken>()),
                Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyBulkApplicationEnvVarsAsync()
        {
            string inputApplicationUuid = GetRandomString();

            IEnumerable<EnvironmentVariable> inputEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable());

            IEnumerable<EnvironmentVariable> randomEnvironmentVariables =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironmentVariable());

            this.applicationServiceMock
                .Setup(service => service.ModifyBulkApplicationEnvVarsAsync(
                    inputApplicationUuid, inputEnvironmentVariables, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironmentVariables);

            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.applicationClient.ModifyBulkApplicationEnvVarsAsync(
                    inputApplicationUuid, inputEnvironmentVariables);

            actualEnvironmentVariables.Should().BeEquivalentTo(randomEnvironmentVariables);

            this.applicationServiceMock.Verify(service =>
                service.ModifyBulkApplicationEnvVarsAsync(
                    inputApplicationUuid, inputEnvironmentVariables, It.IsAny<CancellationToken>()),
                Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveApplicationEnvVarAsync()
        {
            string inputApplicationUuid = GetRandomString();
            string inputEnvironmentVariableUuid = GetRandomString();

            this.applicationServiceMock
                .Setup(service => service.RemoveApplicationEnvVarAsync(
                    inputApplicationUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationClient.RemoveApplicationEnvVarAsync(inputApplicationUuid, inputEnvironmentVariableUuid);

            this.applicationServiceMock.Verify(service =>
                service.RemoveApplicationEnvVarAsync(
                    inputApplicationUuid, inputEnvironmentVariableUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStartApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.applicationServiceMock
                .Setup(service => service.StartApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationClient.StartApplicationAsync(inputApplicationUuid);

            this.applicationServiceMock.Verify(service =>
                service.StartApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldStopApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.applicationServiceMock
                .Setup(service => service.StopApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationClient.StopApplicationAsync(inputApplicationUuid);

            this.applicationServiceMock.Verify(service =>
                service.StopApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRestartApplicationAsync()
        {
            string inputApplicationUuid = GetRandomString();

            this.applicationServiceMock
                .Setup(service => service.RestartApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.applicationClient.RestartApplicationAsync(inputApplicationUuid);

            this.applicationServiceMock.Verify(service =>
                service.RestartApplicationAsync(inputApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }
    }
}
