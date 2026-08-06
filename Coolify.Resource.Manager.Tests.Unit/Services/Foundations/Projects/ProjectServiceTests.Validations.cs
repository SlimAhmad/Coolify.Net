// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        private static InvalidProjectException CreateInvalidUuidException(string parameterName)
        {
            var invalidProjectException =
                new InvalidProjectException(
                    message: "Invalid project. Please fix the errors and try again.");

            invalidProjectException.UpsertDataList(key: parameterName, value: "Text is required");

            return invalidProjectException;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenProjectUuidIsInvalidAndLogItAsync(
            string invalidProjectUuid)
        {
            // given
            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: CreateInvalidUuidException("projectUuid"));

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(invalidProjectUuid);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenProjectUuidIsInvalidAndLogItAsync(
            string invalidProjectUuid)
        {
            // given
            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: CreateInvalidUuidException("projectUuid"));

            // when
            ValueTask removeProjectTask =
                this.projectService.RemoveProjectAsync(invalidProjectUuid);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(removeProjectTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveAllEnvironmentsWhenProjectUuidIsInvalidAndLogItAsync(
            string invalidProjectUuid)
        {
            // given
            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: CreateInvalidUuidException("projectUuid"));

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(invalidProjectUuid);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveEnvironmentWhenProjectUuidIsInvalidAndLogItAsync(
            string invalidProjectUuid)
        {
            // given
            string someEnvironmentNameOrUuid = GetRandomString();

            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: CreateInvalidUuidException("projectUuid"));

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(invalidProjectUuid, someEnvironmentNameOrUuid);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(retrieveEnvironmentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveEnvironmentWhenProjectUuidIsInvalidAndLogItAsync(
            string invalidProjectUuid)
        {
            // given
            string someEnvironmentNameOrUuid = GetRandomString();

            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: CreateInvalidUuidException("projectUuid"));

            // when
            ValueTask removeEnvironmentTask =
                this.projectService.RemoveEnvironmentAsync(invalidProjectUuid, someEnvironmentNameOrUuid);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(removeEnvironmentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenProjectIsNullAndLogItAsync()
        {
            // given
            Project nullProject = null;

            var nullProjectException = new NullProjectException(message: "Project is null.");

            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: nullProjectException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(nullProject);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(addProjectTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnAddWhenProjectNameIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidProject = new Project { Name = invalidText };

            var invalidProjectException =
                new InvalidProjectException(
                    message: "Invalid project. Please fix the errors and try again.");

            invalidProjectException.UpsertDataList(key: nameof(Project.Name), value: "Text is required");

            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: invalidProjectException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(invalidProject);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(addProjectTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyWhenProjectIsNullAndLogItAsync()
        {
            // given
            Project nullProject = null;

            var nullProjectException = new NullProjectException(message: "Project is null.");

            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: nullProjectException);

            // when
            ValueTask<Project> modifyProjectTask =
                this.projectService.ModifyProjectAsync(nullProject);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(modifyProjectTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddEnvironmentWhenEnvironmentIsNullAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment nullEnvironment = null;

            var nullProjectException = new NullProjectException(message: "Environment is null.");

            var expectedProjectValidationException =
                new ProjectValidationException(
                    message: "Project validation error occurred, fix the errors and try again.",
                    innerException: nullProjectException);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, nullEnvironment);

            ProjectValidationException actualException =
                await Assert.ThrowsAsync<ProjectValidationException>(addEnvironmentTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedProjectValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
