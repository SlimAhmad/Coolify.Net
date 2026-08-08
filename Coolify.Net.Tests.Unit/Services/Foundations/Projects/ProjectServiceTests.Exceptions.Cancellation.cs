// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Projects;
using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Foundations.Projects.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Projects
{
    public partial class ProjectServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            // given
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync(CancellationToken.None);

            // then
            await Assert.ThrowsAsync<ProjectDependencyException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            // given
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // when
            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectService.RetrieveAllProjectsAsync(cancellationTokenSource.Token);

            // then
            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllProjectsTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddWhenInfrastructureTimeoutOccursAsync()
        {
            // given
            Project someProject = CreateRandomProject();
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostProjectAsync(
                    It.IsAny<ExternalProject>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject, CancellationToken.None);

            // then
            await Assert.ThrowsAsync<ProjectDependencyException>(addProjectTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostProjectAsync(It.IsAny<ExternalProject>(), It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnAddWhenCallerCancelsAsync()
        {
            // given
            Project someProject = CreateRandomProject();
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            // when
            ValueTask<Project> addProjectTask =
                this.projectService.AddProjectAsync(someProject, cancellationTokenSource.Token);

            // then
            await Assert.ThrowsAsync<OperationCanceledException>(addProjectTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
