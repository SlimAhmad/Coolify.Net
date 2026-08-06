// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Projects;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Projects
{
    public partial class ProjectClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllProjectsAsync()
        {
            IEnumerable<Project> randomProjects =
                Enumerable.Range(0, 3).Select(_ => CreateRandomProject());

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync())
                .ReturnsAsync(randomProjects);

            IEnumerable<Project> actualProjects =
                await this.projectClient.RetrieveAllProjectsAsync();

            actualProjects.Should().BeEquivalentTo(randomProjects);

            this.projectServiceMock.Verify(service => service.RetrieveAllProjectsAsync(), Times.Once);
            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveProjectByUuidAsync()
        {
            Project randomProject = CreateRandomProject();
            string inputProjectUuid = randomProject.Uuid;

            this.projectServiceMock
                .Setup(service => service.RetrieveProjectByUuidAsync(inputProjectUuid))
                .ReturnsAsync(randomProject);

            Project actualProject =
                await this.projectClient.RetrieveProjectByUuidAsync(inputProjectUuid);

            actualProject.Should().BeEquivalentTo(randomProject);

            this.projectServiceMock.Verify(
                service => service.RetrieveProjectByUuidAsync(inputProjectUuid), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddProjectAsync()
        {
            Project inputProject = CreateRandomProject();
            Project randomProject = CreateRandomProject();

            this.projectServiceMock
                .Setup(service => service.AddProjectAsync(inputProject))
                .ReturnsAsync(randomProject);

            Project actualProject = await this.projectClient.AddProjectAsync(inputProject);

            actualProject.Should().BeEquivalentTo(randomProject);

            this.projectServiceMock.Verify(service => service.AddProjectAsync(inputProject), Times.Once);
            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyProjectAsync()
        {
            Project inputProject = CreateRandomProject();
            Project randomProject = CreateRandomProject();

            this.projectServiceMock
                .Setup(service => service.ModifyProjectAsync(inputProject))
                .ReturnsAsync(randomProject);

            Project actualProject = await this.projectClient.ModifyProjectAsync(inputProject);

            actualProject.Should().BeEquivalentTo(randomProject);

            this.projectServiceMock.Verify(service => service.ModifyProjectAsync(inputProject), Times.Once);
            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveProjectAsync()
        {
            string inputProjectUuid = GetRandomString();

            this.projectServiceMock
                .Setup(service => service.RemoveProjectAsync(inputProjectUuid))
                .Returns(ValueTask.CompletedTask);

            await this.projectClient.RemoveProjectAsync(inputProjectUuid);

            this.projectServiceMock.Verify(service => service.RemoveProjectAsync(inputProjectUuid), Times.Once);
            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllEnvironmentsAsync()
        {
            string inputProjectUuid = GetRandomString();
            IEnumerable<CoolifyEnvironment> randomEnvironments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomEnvironment());

            this.projectServiceMock
                .Setup(service => service.RetrieveAllEnvironmentsAsync(inputProjectUuid))
                .ReturnsAsync(randomEnvironments);

            IEnumerable<CoolifyEnvironment> actualEnvironments =
                await this.projectClient.RetrieveAllEnvironmentsAsync(inputProjectUuid);

            actualEnvironments.Should().BeEquivalentTo(randomEnvironments);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllEnvironmentsAsync(inputProjectUuid), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddEnvironmentAsync()
        {
            string inputProjectUuid = GetRandomString();
            CoolifyEnvironment inputEnvironment = CreateRandomEnvironment();
            CoolifyEnvironment randomEnvironment = CreateRandomEnvironment();

            this.projectServiceMock
                .Setup(service => service.AddEnvironmentAsync(inputProjectUuid, inputEnvironment))
                .ReturnsAsync(randomEnvironment);

            CoolifyEnvironment actualEnvironment =
                await this.projectClient.AddEnvironmentAsync(inputProjectUuid, inputEnvironment);

            actualEnvironment.Should().BeEquivalentTo(randomEnvironment);

            this.projectServiceMock.Verify(
                service => service.AddEnvironmentAsync(inputProjectUuid, inputEnvironment), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveEnvironmentAsync()
        {
            string inputProjectUuid = GetRandomString();
            CoolifyEnvironment randomEnvironment = CreateRandomEnvironment();
            string inputEnvironmentNameOrUuid = randomEnvironment.Uuid;

            this.projectServiceMock
                .Setup(service => service.RetrieveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid))
                .ReturnsAsync(randomEnvironment);

            CoolifyEnvironment actualEnvironment =
                await this.projectClient.RetrieveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid);

            actualEnvironment.Should().BeEquivalentTo(randomEnvironment);

            this.projectServiceMock.Verify(service =>
                service.RetrieveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveEnvironmentAsync()
        {
            string inputProjectUuid = GetRandomString();
            string inputEnvironmentNameOrUuid = GetRandomString();

            this.projectServiceMock
                .Setup(service => service.RemoveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid))
                .Returns(ValueTask.CompletedTask);

            await this.projectClient.RemoveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid);

            this.projectServiceMock.Verify(service =>
                service.RemoveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }
    }
}
