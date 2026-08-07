// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Models.Processings.Databases.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Databases
{
    public partial class DatabaseProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseProcessingService.RetrieveAllDatabasesAsync(CancellationToken.None);

            await Assert.ThrowsAsync<DatabaseProcessingDependencyException>(retrieveAllDatabasesTask.AsTask);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseProcessingService.RetrieveAllDatabasesAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllDatabasesTask.AsTask);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
