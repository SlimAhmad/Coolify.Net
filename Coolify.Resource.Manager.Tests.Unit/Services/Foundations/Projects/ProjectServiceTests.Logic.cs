// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Externals.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllProjectsAsync()
        {
            // given
            List<ExternalProject> randomExternalProjects =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalProject()).ToList();

            IEnumerable<Project> expectedProjects = randomExternalProjects.Select(ConvertToProject);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ReturnsAsync(randomExternalProjects);

            // when
            IEnumerable<Project> actualProjects =
                await this.projectService.RetrieveAllProjectsAsync();

            // then
            actualProjects.Should().BeEquivalentTo(expectedProjects);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllProjectsAsync(), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveProjectByUuidAsync()
        {
            // given
            ExternalProject randomExternalProject = CreateRandomExternalProject();
            string inputProjectUuid = randomExternalProject.Uuid;
            Project expectedProject = ConvertToProject(randomExternalProject);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(inputProjectUuid))
                .ReturnsAsync(randomExternalProject);

            // when
            Project actualProject =
                await this.projectService.RetrieveProjectByUuidAsync(inputProjectUuid);

            // then
            actualProject.Should().BeEquivalentTo(expectedProject);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetProjectByUuidAsync(inputProjectUuid), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddProjectAsync()
        {
            // given
            Project inputProject = CreateRandomProject();
            ExternalProject inputExternalProject = ConvertToExternalProject(inputProject);
            ExternalProject returnedExternalProject = CreateRandomExternalProject();
            Project expectedProject = ConvertToProject(returnedExternalProject);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(
                    It.Is<ExternalProject>(external => IsSameExternalProject(external, inputExternalProject))))
                .ReturnsAsync(returnedExternalProject);

            // when
            Project actualProject =
                await this.projectService.AddProjectAsync(inputProject);

            // then
            actualProject.Should().BeEquivalentTo(expectedProject);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostProjectAsync(
                    It.Is<ExternalProject>(external => IsSameExternalProject(external, inputExternalProject))),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldModifyProjectAsync()
        {
            // given
            Project inputProject = CreateRandomProject();
            ExternalProject inputExternalProject = ConvertToExternalProject(inputProject);
            ExternalProject returnedExternalProject = CreateRandomExternalProject();
            Project expectedProject = ConvertToProject(returnedExternalProject);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchProjectAsync(
                    It.Is<ExternalProject>(external => IsSameExternalProject(external, inputExternalProject))))
                .ReturnsAsync(returnedExternalProject);

            // when
            Project actualProject =
                await this.projectService.ModifyProjectAsync(inputProject);

            // then
            actualProject.Should().BeEquivalentTo(expectedProject);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchProjectAsync(
                    It.Is<ExternalProject>(external => IsSameExternalProject(external, inputExternalProject))),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveProjectAsync()
        {
            // given
            string inputProjectUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteProjectAsync(inputProjectUuid))
                .Returns(ValueTask.CompletedTask);

            // when
            await this.projectService.RemoveProjectAsync(inputProjectUuid);

            // then
            this.coolifyApiBrokerMock.Verify(broker => broker.DeleteProjectAsync(inputProjectUuid), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveAllEnvironmentsAsync()
        {
            // given
            string inputProjectUuid = GetRandomString();

            List<ExternalCoolifyEnvironment> randomExternalEnvironments =
                Enumerable.Range(0, 3).Select(_ => CreateRandomExternalEnvironment()).ToList();

            IEnumerable<CoolifyEnvironment> expectedEnvironments =
                randomExternalEnvironments.Select(ConvertToEnvironment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(inputProjectUuid))
                .ReturnsAsync(randomExternalEnvironments);

            // when
            IEnumerable<CoolifyEnvironment> actualEnvironments =
                await this.projectService.RetrieveAllEnvironmentsAsync(inputProjectUuid);

            // then
            actualEnvironments.Should().BeEquivalentTo(expectedEnvironments);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllEnvironmentsAsync(inputProjectUuid), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldAddEnvironmentAsync()
        {
            // given
            string inputProjectUuid = GetRandomString();
            CoolifyEnvironment inputEnvironment = CreateRandomEnvironment();
            ExternalCoolifyEnvironment inputExternalEnvironment = ConvertToExternalEnvironment(inputEnvironment);
            ExternalCoolifyEnvironment returnedExternalEnvironment = CreateRandomExternalEnvironment();
            CoolifyEnvironment expectedEnvironment = ConvertToEnvironment(returnedExternalEnvironment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    inputProjectUuid,
                    It.Is<ExternalCoolifyEnvironment>(external =>
                        IsSameExternalEnvironment(external, inputExternalEnvironment))))
                .ReturnsAsync(returnedExternalEnvironment);

            // when
            CoolifyEnvironment actualEnvironment =
                await this.projectService.AddEnvironmentAsync(inputProjectUuid, inputEnvironment);

            // then
            actualEnvironment.Should().BeEquivalentTo(expectedEnvironment);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(
                    inputProjectUuid,
                    It.Is<ExternalCoolifyEnvironment>(external =>
                        IsSameExternalEnvironment(external, inputExternalEnvironment))),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRetrieveEnvironmentAsync()
        {
            // given
            string inputProjectUuid = GetRandomString();
            ExternalCoolifyEnvironment randomExternalEnvironment = CreateRandomExternalEnvironment();
            string inputEnvironmentNameOrUuid = randomExternalEnvironment.Uuid;
            CoolifyEnvironment expectedEnvironment = ConvertToEnvironment(randomExternalEnvironment);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid))
                .ReturnsAsync(randomExternalEnvironment);

            // when
            CoolifyEnvironment actualEnvironment =
                await this.projectService.RetrieveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid);

            // then
            actualEnvironment.Should().BeEquivalentTo(expectedEnvironment);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveEnvironmentAsync()
        {
            // given
            string inputProjectUuid = GetRandomString();
            string inputEnvironmentNameOrUuid = GetRandomString();

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid))
                .Returns(ValueTask.CompletedTask);

            // when
            await this.projectService.RemoveEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid);

            // then
            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteEnvironmentAsync(inputProjectUuid, inputEnvironmentNameOrUuid), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        private static bool IsSameExternalProject(ExternalProject actual, ExternalProject expected) =>
            actual.Uuid == expected.Uuid
            && actual.Name == expected.Name
            && actual.Description == expected.Description;

        private static bool IsSameExternalEnvironment(
            ExternalCoolifyEnvironment actual, ExternalCoolifyEnvironment expected) =>
                actual.Uuid == expected.Uuid
                && actual.Name == expected.Name
                && actual.Description == expected.Description
                && actual.ProjectUuid == expected.ProjectUuid;
    }
}
