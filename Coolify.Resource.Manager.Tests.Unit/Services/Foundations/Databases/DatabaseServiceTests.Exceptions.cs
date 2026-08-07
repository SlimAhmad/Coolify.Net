// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveAllDatabasesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

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
                .Setup(broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllDatabasesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

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
                .Setup(broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllDatabasesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseServiceException>(retrieveAllDatabasesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenInfrastructureTimeoutOccursAsync()
        {
            var infrastructureTimeoutException = new OperationCanceledException("Infrastructure timeout.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(infrastructureTimeoutException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseService.RetrieveAllDatabasesAsync(CancellationToken.None);

            await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveAllDatabasesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReThrowCleanlyOnRetrieveAllWhenCallerCancelsAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseService.RetrieveAllDatabasesAsync(cancellationTokenSource.Token);

            await Assert.ThrowsAsync<OperationCanceledException>(retrieveAllDatabasesTask.AsTask);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
