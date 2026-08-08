// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Projects;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Foundations.Projects.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Project someProject = CreateRandomProject();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateInvalidProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(addProjectTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfConflictErrorOccursAndLogItAsync()
        {
            // given
            Project someProject = CreateRandomProject();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateAlreadyExistsProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(addProjectTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            Project someProject = CreateRandomProject();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(addProjectTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            Project someProject = CreateRandomProject();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(addProjectTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            Project someProject = CreateRandomProject();
            var httpRequestException = new HttpRequestException("Network failure.");

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(addProjectTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Project someProject = CreateRandomProject();
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject);

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(addProjectTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
