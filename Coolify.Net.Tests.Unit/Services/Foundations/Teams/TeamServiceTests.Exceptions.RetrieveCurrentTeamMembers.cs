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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveCurrentTeamMembersIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            TeamDependencyValidationException expectedException =
                CreateInvalidTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveCurrentTeamMembersTask =
                this.teamService.RetrieveCurrentTeamMembersAsync();

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveCurrentTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamMembersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveCurrentTeamMembersIfConflictErrorOccursAndLogItAsync()
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            TeamDependencyValidationException expectedException =
                CreateAlreadyExistsTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveCurrentTeamMembersTask =
                this.teamService.RetrieveCurrentTeamMembersAsync();

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveCurrentTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamMembersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveCurrentTeamMembersIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveCurrentTeamMembersTask =
                this.teamService.RetrieveCurrentTeamMembersAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveCurrentTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamMembersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveCurrentTeamMembersIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveCurrentTeamMembersTask =
                this.teamService.RetrieveCurrentTeamMembersAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveCurrentTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamMembersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveCurrentTeamMembersIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            
            var httpRequestException = new HttpRequestException("Network failure.");

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync())
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveCurrentTeamMembersTask =
                this.teamService.RetrieveCurrentTeamMembersAsync();

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveCurrentTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamMembersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveCurrentTeamMembersIfServiceErrorOccursAndLogItAsync()
        {
            // given
            
            var exception = new Exception("Unexpected error.");

            TeamServiceException expectedException =
                CreateFailedTeamServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetCurrentTeamMembersAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveCurrentTeamMembersTask =
                this.teamService.RetrieveCurrentTeamMembersAsync();

            TeamServiceException actualException =
                await Assert.ThrowsAsync<TeamServiceException>(retrieveCurrentTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetCurrentTeamMembersAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
