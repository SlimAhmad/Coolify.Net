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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveEnvironmentIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateInvalidProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveEnvironmentTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveEnvironmentIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateAlreadyExistsProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveEnvironmentTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveEnvironmentIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveEnvironmentIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveEnvironmentIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(retrieveEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveEnvironmentIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(retrieveEnvironmentTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
