// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Externals.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Models.Foundations.Projects.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllProjectsAsync(), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllProjectsAsync(), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllProjectsAsync(), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllProjectsAsync(), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync())
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectServiceException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllProjectsAsync(), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnAddWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            Project someProject = CreateRandomProject();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<Project> addProjectTask = this.projectService.AddProjectAsync(someProject);

            await Assert.ThrowsAsync<ProjectDependencyException>(addProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            Project someProject = CreateRandomProject();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(exception);

            ValueTask<Project> addProjectTask = this.projectService.AddProjectAsync(someProject);

            await Assert.ThrowsAsync<ProjectServiceException>(addProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PostProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnModifyWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            Project someProject = CreateRandomProject();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<Project> modifyProjectTask = this.projectService.ModifyProjectAsync(someProject);

            await Assert.ThrowsAsync<ProjectDependencyException>(modifyProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyWhenExceptionOccursAsync()
        {
            Project someProject = CreateRandomProject();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchProjectAsync(It.IsAny<ExternalProject>()))
                .ThrowsAsync(exception);

            ValueTask<Project> modifyProjectTask = this.projectService.ModifyProjectAsync(someProject);

            await Assert.ThrowsAsync<ProjectServiceException>(modifyProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.PatchProjectAsync(It.IsAny<ExternalProject>()), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteProjectAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            ValueTask removeProjectTask = this.projectService.RemoveProjectAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectDependencyException>(removeProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.DeleteProjectAsync(someProjectUuid), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteProjectAsync(someProjectUuid))
                .ThrowsAsync(exception);

            ValueTask removeProjectTask = this.projectService.RemoveProjectAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectServiceException>(removeProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.DeleteProjectAsync(someProjectUuid), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllEnvironmentsWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllEnvironmentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllEnvironmentsWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllEnvironmentsAsync(someProjectUuid))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<CoolifyEnvironment>> retrieveAllEnvironmentsTask =
                this.projectService.RetrieveAllEnvironmentsAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectServiceException>(retrieveAllEnvironmentsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetAllEnvironmentsAsync(someProjectUuid), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnAddEnvironmentWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            await Assert.ThrowsAsync<ProjectDependencyException>(addEnvironmentTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddEnvironmentWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostEnvironmentAsync(
                    someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()))
                .ThrowsAsync(exception);

            ValueTask<CoolifyEnvironment> addEnvironmentTask =
                this.projectService.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            await Assert.ThrowsAsync<ProjectServiceException>(addEnvironmentTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostEnvironmentAsync(someProjectUuid, It.IsAny<ExternalCoolifyEnvironment>()), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveEnvironmentWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            await Assert.ThrowsAsync<ProjectDependencyException>(retrieveEnvironmentTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveEnvironmentWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(exception);

            ValueTask<CoolifyEnvironment> retrieveEnvironmentTask =
                this.projectService.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            await Assert.ThrowsAsync<ProjectServiceException>(retrieveEnvironmentTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemoveEnvironmentWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(httpRequestException);

            ValueTask removeEnvironmentTask =
                this.projectService.RemoveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            await Assert.ThrowsAsync<ProjectDependencyException>(removeEnvironmentTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveEnvironmentWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid))
                .ThrowsAsync(exception);

            ValueTask removeEnvironmentTask =
                this.projectService.RemoveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            await Assert.ThrowsAsync<ProjectServiceException>(removeEnvironmentTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenHttpBadRequestOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(httpRequestException);

            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectDependencyValidationException>(retrieveProjectByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByUuidWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetProjectByUuidAsync(someProjectUuid))
                .ThrowsAsync(exception);

            ValueTask<Project> retrieveProjectByUuidTask =
                this.projectService.RetrieveProjectByUuidAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectServiceException>(retrieveProjectByUuidTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker => broker.GetProjectByUuidAsync(someProjectUuid), Times.Once);
            this.loggingBrokerMock.Verify(broker => broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);
            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
