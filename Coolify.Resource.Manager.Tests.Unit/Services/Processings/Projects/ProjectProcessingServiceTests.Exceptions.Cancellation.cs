// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Projects;
using Coolify.Resource.Manager.Models.Processings.Projects.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Projects
{
    public partial class ProjectProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectProcessingService.RetrieveAllProjectsAsync(CancellationToken.None);

            await Assert.ThrowsAsync<ProjectProcessingDependencyException>(retrieveAllProjectsTask.AsTask);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectProcessingService.RetrieveAllProjectsAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllProjectsTask.AsTask);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
