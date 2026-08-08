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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllEnvironmentsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateInvalidProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllEnvironmentsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateAlreadyExistsProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllEnvironmentsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllEnvironmentsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllEnvironmentsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllEnvironmentsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(retrieveAllEnvironmentsTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
