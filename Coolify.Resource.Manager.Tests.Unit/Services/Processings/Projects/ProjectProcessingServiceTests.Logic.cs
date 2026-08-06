// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Projects;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Projects
{
    public partial class ProjectProcessingServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllProjectsAsync()
        {
            IEnumerable<Project> randomProjects = Enumerable.Range(0, 3).Select(_ => CreateRandomProject());

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomProjects);

            IEnumerable<Project> actualProjects = await this.projectProcessingService.RetrieveAllProjectsAsync();

            actualProjects.Should().BeEquivalentTo(randomProjects);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveProjectByUuidAsync()
        {
            Project randomProject = CreateRandomProject();
            string inputProjectUuid = randomProject.Uuid;

            this.projectServiceMock
                .Setup(service => service.RetrieveProjectByUuidAsync(inputProjectUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomProject);

            Project actualProject = await this.projectProcessingService.RetrieveProjectByUuidAsync(inputProjectUuid);

            actualProject.Should().BeEquivalentTo(randomProject);

            this.projectServiceMock.Verify(service =>
                service.RetrieveProjectByUuidAsync(inputProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddProjectAsync()
        {
            Project inputProject = CreateRandomProject();
            Project randomProject = CreateRandomProject();

            this.projectServiceMock
                .Setup(service => service.AddProjectAsync(inputProject, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomProject);

            Project actualProject = await this.projectProcessingService.AddProjectAsync(inputProject);

            actualProject.Should().BeEquivalentTo(randomProject);

            this.projectServiceMock.Verify(
                service => service.AddProjectAsync(inputProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyProjectAsync()
        {
            Project inputProject = CreateRandomProject();
            Project randomProject = CreateRandomProject();

            this.projectServiceMock
                .Setup(service => service.ModifyProjectAsync(inputProject, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomProject);

            Project actualProject = await this.projectProcessingService.ModifyProjectAsync(inputProject);

            actualProject.Should().BeEquivalentTo(randomProject);

            this.projectServiceMock.Verify(
                service => service.ModifyProjectAsync(inputProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveProjectAsync()
        {
            string inputProjectUuid = GetRandomString();

            this.projectServiceMock
                .Setup(service => service.RemoveProjectAsync(inputProjectUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.projectProcessingService.RemoveProjectAsync(inputProjectUuid);

            this.projectServiceMock.Verify(
                service => service.RemoveProjectAsync(inputProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllEnvironmentsAsync()
        {
            string inputProjectUuid = GetRandomString();
            IEnumerable<CoolifyEnvironment> randomEnvironments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironment());

            this.projectServiceMock
                .Setup(service => service.RetrieveAllEnvironmentsAsync(inputProjectUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironments);

            IEnumerable<CoolifyEnvironment> actualEnvironments =
                await this.projectProcessingService.RetrieveAllEnvironmentsAsync(inputProjectUuid);

            actualEnvironments.Should().BeEquivalentTo(randomEnvironments);

            this.projectServiceMock.Verify(service =>
                service.RetrieveAllEnvironmentsAsync(inputProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddEnvironmentAsync()
        {
            string inputProjectUuid = GetRandomString();
            CoolifyEnvironment inputEnvironment = CreateRandomEnvironment();
            CoolifyEnvironment randomEnvironment = CreateRandomEnvironment();

            this.projectServiceMock
                .Setup(service => service.AddEnvironmentAsync(
                    inputProjectUuid, inputEnvironment, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironment);

            CoolifyEnvironment actualEnvironment =
                await this.projectProcessingService.AddEnvironmentAsync(inputProjectUuid, inputEnvironment);

            actualEnvironment.Should().BeEquivalentTo(randomEnvironment);

            this.projectServiceMock.Verify(service =>
                service.AddEnvironmentAsync(inputProjectUuid, inputEnvironment, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveEnvironmentAsync()
        {
            string inputProjectUuid = GetRandomString();
            CoolifyEnvironment randomEnvironment = CreateRandomEnvironment();
            string inputEnvironmentNameOrUuid = randomEnvironment.Uuid;

            this.projectServiceMock
                .Setup(service => service.RetrieveEnvironmentAsync(
                    inputProjectUuid, inputEnvironmentNameOrUuid, It.IsAny<CancellationToken>()))
                .ReturnsAsync(randomEnvironment);

            CoolifyEnvironment actualEnvironment =
                await this.projectProcessingService.RetrieveEnvironmentAsync(
                    inputProjectUuid, inputEnvironmentNameOrUuid);

            actualEnvironment.Should().BeEquivalentTo(randomEnvironment);

            this.projectServiceMock.Verify(service =>
                service.RetrieveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveEnvironmentAsync()
        {
            string inputProjectUuid = GetRandomString();
            string inputEnvironmentNameOrUuid = GetRandomString();

            this.projectServiceMock
                .Setup(service => service.RemoveEnvironmentAsync(
                    inputProjectUuid, inputEnvironmentNameOrUuid, It.IsAny<CancellationToken>()))
                .Returns(ValueTask.CompletedTask);

            await this.projectProcessingService.RemoveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid);

            this.projectServiceMock.Verify(service =>
                service.RemoveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
