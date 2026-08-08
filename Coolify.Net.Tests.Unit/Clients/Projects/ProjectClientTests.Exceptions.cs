// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Projects.Exceptions;
using Coolify.Net.Models.Foundations.Projects;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Projects
{
    public partial class ProjectClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<Project>> task = this.projectClient.RetrieveAllProjectsAsync();

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new ProjectClientDependencyException(
                message: "Project client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<Project>> task = this.projectClient.RetrieveAllProjectsAsync();

            ProjectClientDependencyException actual =
                await Assert.ThrowsAsync<ProjectClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Project>> task = this.projectClient.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<Project>> task = this.projectClient.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnAddWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            Project someProject = CreateRandomProject();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.AddProjectAsync(someProject, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Project> task = this.projectClient.AddProjectAsync(someProject);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(
                service => service.AddProjectAsync(someProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnAddWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            Project someProject = CreateRandomProject();

            var expected = new ProjectClientDependencyException(
                message: "Project client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.AddProjectAsync(someProject, It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<Project> task = this.projectClient.AddProjectAsync(someProject);

            ProjectClientDependencyException actual =
                await Assert.ThrowsAsync<ProjectClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(
                service => service.AddProjectAsync(someProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            Project someProject = CreateRandomProject();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.AddProjectAsync(someProject, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Project> task = this.projectClient.AddProjectAsync(someProject);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(
                service => service.AddProjectAsync(someProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnModifyWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            Project someProject = CreateRandomProject();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.ModifyProjectAsync(someProject, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Project> task = this.projectClient.ModifyProjectAsync(someProject);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(
                service => service.ModifyProjectAsync(someProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnModifyWhenExceptionOccursAsync()
        {
            Project someProject = CreateRandomProject();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.ModifyProjectAsync(someProject, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Project> task = this.projectClient.ModifyProjectAsync(someProject);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(
                service => service.ModifyProjectAsync(someProject, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRemoveWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someProjectUuid = GetRandomString();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RemoveProjectAsync(someProjectUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.projectClient.RemoveProjectAsync(someProjectUuid);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(
                service => service.RemoveProjectAsync(someProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RemoveProjectAsync(someProjectUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.projectClient.RemoveProjectAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(
                service => service.RemoveProjectAsync(someProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllEnvironmentsWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someProjectUuid = GetRandomString();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RetrieveAllEnvironmentsAsync(someProjectUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<CoolifyEnvironment>> task =
                this.projectClient.RetrieveAllEnvironmentsAsync(someProjectUuid);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(service =>
                service.RetrieveAllEnvironmentsAsync(someProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllEnvironmentsWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RetrieveAllEnvironmentsAsync(someProjectUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<CoolifyEnvironment>> task =
                this.projectClient.RetrieveAllEnvironmentsAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(service =>
                service.RetrieveAllEnvironmentsAsync(someProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnAddEnvironmentWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.AddEnvironmentAsync(
                    someProjectUuid, someEnvironment, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<CoolifyEnvironment> task =
                this.projectClient.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(service =>
                service.AddEnvironmentAsync(someProjectUuid, someEnvironment, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnAddEnvironmentWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            CoolifyEnvironment someEnvironment = CreateRandomEnvironment();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.AddEnvironmentAsync(
                    someProjectUuid, someEnvironment, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<CoolifyEnvironment> task =
                this.projectClient.AddEnvironmentAsync(someProjectUuid, someEnvironment);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(service =>
                service.AddEnvironmentAsync(someProjectUuid, someEnvironment, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveEnvironmentWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RetrieveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<CoolifyEnvironment> task =
                this.projectClient.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(service =>
                service.RetrieveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveEnvironmentWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RetrieveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<CoolifyEnvironment> task =
                this.projectClient.RetrieveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(service =>
                service.RetrieveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRemoveEnvironmentWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RemoveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task =
                this.projectClient.RemoveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(service =>
                service.RemoveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRemoveEnvironmentWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            string someEnvironmentNameOrUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RemoveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task =
                this.projectClient.RemoveEnvironmentAsync(someProjectUuid, someEnvironmentNameOrUuid);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(service =>
                service.RemoveEnvironmentAsync(
                    someProjectUuid, someEnvironmentNameOrUuid, It.IsAny<CancellationToken>()),
                Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveByUuidWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someProjectUuid = GetRandomString();

            var expected = new ProjectClientValidationException(
                message: "Project client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.projectServiceMock
                .Setup(service => service.RetrieveProjectByUuidAsync(someProjectUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Project> task = this.projectClient.RetrieveProjectByUuidAsync(someProjectUuid);

            ProjectClientValidationException actual =
                await Assert.ThrowsAsync<ProjectClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.projectServiceMock.Verify(service =>
                service.RetrieveProjectByUuidAsync(someProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveByUuidWhenExceptionOccursAsync()
        {
            string someProjectUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RetrieveProjectByUuidAsync(someProjectUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Project> task = this.projectClient.RetrieveProjectByUuidAsync(someProjectUuid);

            await Assert.ThrowsAsync<ProjectClientServiceException>(task.AsTask);

            this.projectServiceMock.Verify(service =>
                service.RetrieveProjectByUuidAsync(someProjectUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
        }
    }
}
