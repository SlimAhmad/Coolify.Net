// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Databases;
using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Models.Foundations.Databases.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Databases
{
    public partial class DatabaseServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddDragonflyDatabaseIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new DragonflyDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            DatabaseDependencyValidationException expectedException =
                CreateInvalidDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DragonflyDatabase> addDragonflyDatabaseTask =
                this.databaseService.AddDragonflyDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addDragonflyDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddDragonflyDatabaseIfConflictErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new DragonflyDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            DatabaseDependencyValidationException expectedException =
                CreateAlreadyExistsDatabaseDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DragonflyDatabase> addDragonflyDatabaseTask =
                this.databaseService.AddDragonflyDatabaseAsync(someDatabase);

            DatabaseDependencyValidationException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyValidationException>(addDragonflyDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddDragonflyDatabaseIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new DragonflyDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DragonflyDatabase> addDragonflyDatabaseTask =
                this.databaseService.AddDragonflyDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addDragonflyDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddDragonflyDatabaseIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            var someDatabase = new DragonflyDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DragonflyDatabase> addDragonflyDatabaseTask =
                this.databaseService.AddDragonflyDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addDragonflyDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddDragonflyDatabaseIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var someDatabase = new DragonflyDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var httpRequestException = new HttpRequestException("Network failure.");

            DatabaseDependencyException expectedException =
                CreateFailedDatabaseDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<DragonflyDatabase> addDragonflyDatabaseTask =
                this.databaseService.AddDragonflyDatabaseAsync(someDatabase);

            DatabaseDependencyException actualException =
                await Assert.ThrowsAsync<DatabaseDependencyException>(addDragonflyDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddDragonflyDatabaseIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var someDatabase = new DragonflyDatabase { Uuid = GetRandomString(), Name = GetRandomString(), ServerUuid = GetRandomString(), ProjectUuid = GetRandomString() };
            var exception = new Exception("Unexpected error.");

            DatabaseServiceException expectedException =
                CreateFailedDatabaseServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<DragonflyDatabase> addDragonflyDatabaseTask =
                this.databaseService.AddDragonflyDatabaseAsync(someDatabase);

            DatabaseServiceException actualException =
                await Assert.ThrowsAsync<DatabaseServiceException>(addDragonflyDatabaseTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostDragonflyDatabaseAsync(It.IsAny<ExternalDragonflyDatabase>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
