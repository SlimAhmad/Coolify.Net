// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Models.Foundations.Teams.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveAllTeamsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

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
                .Setup(broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamDependencyException>(retrieveAllTeamsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

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
                .Setup(broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamDependencyException>(retrieveAllTeamsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamDependencyException>(retrieveAllTeamsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamServiceException>(retrieveAllTeamsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveByIdWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<Team> retrieveByIdTask = this.teamService.RetrieveTeamByIdAsync(someId);

            await Assert.ThrowsAsync<TeamDependencyException>(retrieveByIdTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdWhenExceptionOccursAsync()
        {
            int someId = GetRandomId();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Team> retrieveByIdTask = this.teamService.RetrieveTeamByIdAsync(someId);

            await Assert.ThrowsAsync<TeamServiceException>(retrieveByIdTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
