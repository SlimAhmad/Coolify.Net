// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Foundations.Projects.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateInvalidProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateAlreadyExistsProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(retrieveProjectByUuidTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
