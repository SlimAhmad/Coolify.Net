// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Processings.Projects.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Projects
{
    public partial class ProjectProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenProjectIsNullAndLogItAsync()
        {
            Project nullProject = null;

            var nullProjectProcessingException =
                new NullProjectProcessingException(message: "Project is null.");

            var expectedProjectProcessingValidationException =
                new ProjectProcessingValidationException(
                    message: "Project processing validation error occurred, fix the errors and try again.",
                    innerException: nullProjectProcessingException);

            ValueTask<Project> addProjectTask = this.projectProcessingService.AddProjectAsync(nullProject);

            ProjectProcessingValidationException actualException =
                await Assert.ThrowsAsync<ProjectProcessingValidationException>(addProjectTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedProjectProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenProjectUuidIsInvalidAndLogItAsync(
            string invalidProjectUuid)
        {
            var invalidProjectProcessingException =
                new InvalidProjectProcessingException(message: "Project uuid is invalid.");

            var expectedProjectProcessingValidationException =
                new ProjectProcessingValidationException(
                    message: "Project processing validation error occurred, fix the errors and try again.",
                    innerException: invalidProjectProcessingException);

            ValueTask<Project> retrieveByUuidTask =
                this.projectProcessingService.RetrieveProjectByUuidAsync(invalidProjectUuid);

            ProjectProcessingValidationException actualException =
                await Assert.ThrowsAsync<ProjectProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedProjectProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddEnvironmentWhenEnvironmentIsNullAndLogItAsync()
        {
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment nullEnvironment = null;

            var nullProjectProcessingException =
                new NullProjectProcessingException(message: "Environment is null.");

            var expectedProjectProcessingValidationException =
                new ProjectProcessingValidationException(
                    message: "Project processing validation error occurred, fix the errors and try again.",
                    innerException: nullProjectProcessingException);

            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectProcessingService.AddEnvironmentAsync(someProjectUuid, nullEnvironment);

            ProjectProcessingValidationException actualException =
                await Assert.ThrowsAsync<ProjectProcessingValidationException>(addEnvironmentTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedProjectProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
