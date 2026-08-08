// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveDatabaseByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Database> retrieveDatabaseByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(someDatabaseUuid);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveDatabaseByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveDatabaseByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Database> retrieveDatabaseByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(someDatabaseUuid);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(retrieveDatabaseByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveDatabaseByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Database> retrieveDatabaseByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(someDatabaseUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveDatabaseByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveDatabaseByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someDatabaseUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Database> retrieveDatabaseByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(someDatabaseUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveDatabaseByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveDatabaseByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(someDatabaseUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Database> retrieveDatabaseByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(someDatabaseUuid);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(retrieveDatabaseByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveDatabaseByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someDatabaseUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetDatabaseByUuidAsync(someDatabaseUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Database> retrieveDatabaseByUuidTask =
                this.databaseService.RetrieveDatabaseByUuidAsync(someDatabaseUuid);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(retrieveDatabaseByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetDatabaseByUuidAsync(someDatabaseUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
