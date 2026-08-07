// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Databases;
using Coolify.Resource.Manager.Models.Foundations.Databases.Exceptions;
using Coolify.Resource.Manager.Models.Processings.Databases.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Databases
{
    public partial class DatabaseProcessingServiceTests
    {
        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> FoundationValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DatabaseValidationException("test", inner),
                new DatabaseDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new DatabaseDependencyException("test", inner),
                new DatabaseServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseProcessingService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseProcessingDependencyValidationException>(retrieveAllDatabasesTask.AsTask);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseProcessingService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseProcessingDependencyException>(retrieveAllDatabasesTask.AsTask);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Database>> retrieveAllDatabasesTask =
                this.databaseProcessingService.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseProcessingServiceException>(retrieveAllDatabasesTask.AsTask);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
