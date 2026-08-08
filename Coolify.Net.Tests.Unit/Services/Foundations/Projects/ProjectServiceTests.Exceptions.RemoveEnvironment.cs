// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Projects.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemoveEnvironmentIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ProjectDependencyException expectedProjectDependencyException =
                CreateFailedProjectDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask removeEnvironmentTask =
                this.projectService.RemoveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectDependencyException actualProjectDependencyException =
                await Assert.ThrowsAsync<ProjectDependencyException>(removeEnvironmentTask.AsTask);

            // then
            actualProjectDependencyException.Should()
                .BeEquivalentTo(expectedProjectDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveEnvironmentIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ProjectServiceException expectedProjectServiceException =
                CreateFailedProjectServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask removeEnvironmentTask =
                this.projectService.RemoveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectServiceException actualProjectServiceException =
                await Assert.ThrowsAsync<ProjectServiceException>(removeEnvironmentTask.AsTask);

            // then
            actualProjectServiceException.Should()
                .BeEquivalentTo(expectedProjectServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedProjectServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
