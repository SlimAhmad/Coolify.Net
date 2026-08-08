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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateInvalidProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveAllProjectsTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfConflictErrorOccursAndLogItAsync()
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateAlreadyExistsProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveAllProjectsTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var httpRequestException = new HttpRequestException("Network failure.");

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(retrieveAllProjectsTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
