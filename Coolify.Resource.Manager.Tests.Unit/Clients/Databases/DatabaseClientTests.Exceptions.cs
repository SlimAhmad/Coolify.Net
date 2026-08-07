// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Databases.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Databases;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Databases
{
    public partial class DatabaseClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new DatabaseClientValidationException(
                message: "Database client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<Database>> task = this.databaseClient.RetrieveAllDatabasesAsync();

            DatabaseClientValidationException actual =
                await Assert.ThrowsAsync<DatabaseClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new DatabaseClientDependencyException(
                message: "Database client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<Database>> task = this.databaseClient.RetrieveAllDatabasesAsync();

            DatabaseClientDependencyException actual =
                await Assert.ThrowsAsync<DatabaseClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Database>> task = this.databaseClient.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<DatabaseClientServiceException>(task.AsTask);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.databaseServiceMock
                .Setup(service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<Database>> task = this.databaseClient.RetrieveAllDatabasesAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.databaseServiceMock.Verify(
                service => service.RetrieveAllDatabasesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRemoveWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someDatabaseUuid = GetRandomString();

            var expected = new DatabaseClientValidationException(
                message: "Database client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.databaseServiceMock
                .Setup(service => service.RemoveDatabaseAsync(someDatabaseUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.databaseClient.RemoveDatabaseAsync(someDatabaseUuid);

            DatabaseClientValidationException actual =
                await Assert.ThrowsAsync<DatabaseClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.databaseServiceMock.Verify(service =>
                service.RemoveDatabaseAsync(someDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someDatabaseUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.databaseServiceMock
                .Setup(service => service.RemoveDatabaseAsync(someDatabaseUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.databaseClient.RemoveDatabaseAsync(someDatabaseUuid);

            await Assert.ThrowsAsync<DatabaseClientServiceException>(task.AsTask);

            this.databaseServiceMock.Verify(service =>
                service.RemoveDatabaseAsync(someDatabaseUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.databaseServiceMock.VerifyNoOtherCalls();
        }
    }
}
