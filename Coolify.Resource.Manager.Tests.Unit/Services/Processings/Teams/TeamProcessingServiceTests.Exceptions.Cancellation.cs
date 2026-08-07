// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams;
using Coolify.Resource.Manager.Models.Processings.Teams.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Teams
{
    public partial class TeamProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamProcessingService.RetrieveAllTeamsAsync(CancellationToken.None);

            await Assert.ThrowsAsync<TeamProcessingDependencyException>(retrieveAllTeamsTask.AsTask);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamProcessingService.RetrieveAllTeamsAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllTeamsTask.AsTask);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
