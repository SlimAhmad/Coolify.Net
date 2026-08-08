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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveTeamMembersIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            TeamDependencyValidationException expectedException =
                CreateInvalidTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveTeamMembersTask =
                this.teamService.RetrieveTeamMembersAsync(someId);

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveTeamMembersIfConflictErrorOccursAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            TeamDependencyValidationException expectedException =
                CreateAlreadyExistsTeamDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveTeamMembersTask =
                this.teamService.RetrieveTeamMembersAsync(someId);

            TeamDependencyValidationException actualException =
                await Assert.ThrowsAsync<TeamDependencyValidationException>(retrieveTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveTeamMembersIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveTeamMembersTask =
                this.teamService.RetrieveTeamMembersAsync(someId);

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveTeamMembersIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            int someId = GetRandomId();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveTeamMembersTask =
                this.teamService.RetrieveTeamMembersAsync(someId);

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveTeamMembersIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            var httpRequestException = new HttpRequestException("Network failure.");

            TeamDependencyException expectedException =
                CreateFailedTeamDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(someId))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveTeamMembersTask =
                this.teamService.RetrieveTeamMembersAsync(someId);

            TeamDependencyException actualException =
                await Assert.ThrowsAsync<TeamDependencyException>(retrieveTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveTeamMembersIfServiceErrorOccursAndLogItAsync()
        {
            // given
            int someId = GetRandomId();
            var exception = new Exception("Unexpected error.");

            TeamServiceException expectedException =
                CreateFailedTeamServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetTeamMembersAsync(someId))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<TeamMember>> retrieveTeamMembersTask =
                this.teamService.RetrieveTeamMembersAsync(someId);

            TeamServiceException actualException =
                await Assert.ThrowsAsync<TeamServiceException>(retrieveTeamMembersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetTeamMembersAsync(someId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
