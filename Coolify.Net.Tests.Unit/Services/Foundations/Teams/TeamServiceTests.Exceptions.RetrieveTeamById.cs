// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Models.Foundations.Teams.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveTeamByIdIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            TeamDependencyValidationException expectedException =
                CreateInvalidTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveTeamByIdTask =
                this.teamService.RetrieveTeamByIdAsync(someId);

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveTeamByIdTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveTeamByIdIfConflictErrorOccursAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            TeamDependencyValidationException expectedException =
                CreateAlreadyExistsTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveTeamByIdTask =
                this.teamService.RetrieveTeamByIdAsync(someId);

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveTeamByIdTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveTeamByIdIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveTeamByIdTask =
                this.teamService.RetrieveTeamByIdAsync(someId);

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveTeamByIdTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveTeamByIdIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveTeamByIdTask =
                this.teamService.RetrieveTeamByIdAsync(someId);

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveTeamByIdTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveTeamByIdIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            var httpRequestException = new HttpRequestException("Network failure.");

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveTeamByIdTask =
                this.teamService.RetrieveTeamByIdAsync(someId);

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveTeamByIdTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveTeamByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            var exception = new Exception("Unexpected error.");

            TeamServiceException expectedException =
                CreateFailedTeamServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamByIdAsync(someId))
                .ThrowsAsync(exception);

            // when
            ValueTask<Team> retrieveTeamByIdTask =
                this.teamService.RetrieveTeamByIdAsync(someId);

            TeamServiceException actualException =
                await Assert.ThrowsAsync<TeamServiceException>(retrieveTeamByIdTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamByIdAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
