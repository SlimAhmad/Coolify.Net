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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllTeamsIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            TeamDependencyValidationException expectedException =
                CreateInvalidTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamService.RetrieveAllTeamsAsync();

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveAllTeamsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllTeamsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllTeamsIfConflictErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            TeamDependencyValidationException expectedException =
                CreateAlreadyExistsTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamService.RetrieveAllTeamsAsync();

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveAllTeamsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllTeamsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllTeamsIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamService.RetrieveAllTeamsAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveAllTeamsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllTeamsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllTeamsIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamService.RetrieveAllTeamsAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveAllTeamsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllTeamsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllTeamsIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            
            var httpRequestException = new HttpRequestException("Network failure.");

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamService.RetrieveAllTeamsAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveAllTeamsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllTeamsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllTeamsIfServiceErrorOccursAndLogItAsync()
        {
            // given
            
            var exception = new Exception("Unexpected error.");

            TeamServiceException expectedException =
                CreateFailedTeamServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllTeamsAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask =
                this.teamService.RetrieveAllTeamsAsync();

            TeamServiceException actualException =
                await Assert.ThrowsAsync<TeamServiceException>(retrieveAllTeamsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetAllTeamsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
