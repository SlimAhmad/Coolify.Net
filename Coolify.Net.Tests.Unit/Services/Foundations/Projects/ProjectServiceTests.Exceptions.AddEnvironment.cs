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
        public async Task ShouldThrowDependencyValidationExceptionOnAddEnvironmentIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateInvalidProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(addEnvironmentTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddEnvironmentIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ProjectDependencyValidationException expectedProjectDependencyValidationException =
                CreateAlreadyExistsProjectDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectDependencyValidationException actualProjectDependencyValidationException =
                await Assert.ThrowsAsync<ProjectDependencyValidationException>(addEnvironmentTask.AsTask);

            // then
            actualProjectDependencyValidationException.Should()
                .BeEquivalentTo(expectedProjectDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnAddEnvironmentIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(addEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnAddEnvironmentIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(addEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddEnvironmentIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            var httpRequestException = new HttpRequestException("Network failure.");

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(addEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddEnvironmentIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(addEnvironmentTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
