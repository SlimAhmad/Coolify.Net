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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveCurrentTeamIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            TeamDependencyValidationException expectedException =
                CreateInvalidTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveCurrentTeamTask =
                this.teamService.RetrieveCurrentTeamAsync();

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveCurrentTeamTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveCurrentTeamIfConflictErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            TeamDependencyValidationException expectedException =
                CreateAlreadyExistsTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveCurrentTeamTask =
                this.teamService.RetrieveCurrentTeamAsync();

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveCurrentTeamTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveCurrentTeamIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveCurrentTeamTask =
                this.teamService.RetrieveCurrentTeamAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveCurrentTeamTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveCurrentTeamIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveCurrentTeamTask =
                this.teamService.RetrieveCurrentTeamAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveCurrentTeamTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveCurrentTeamIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            
            var httpRequestException = new HttpRequestException("Network failure.");

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Team> retrieveCurrentTeamTask =
                this.teamService.RetrieveCurrentTeamAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveCurrentTeamTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveCurrentTeamIfServiceErrorOccursAndLogItAsync()
        {
            // given
            
            var exception = new Exception("Unexpected error.");

            TeamServiceException expectedException =
                CreateFailedTeamServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<Team> retrieveCurrentTeamTask =
                this.teamService.RetrieveCurrentTeamAsync();

            TeamServiceException actualException =
                await Assert.ThrowsAsync<TeamServiceException>(retrieveCurrentTeamTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
