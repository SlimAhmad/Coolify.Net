// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams;
using Coolify.Resource.Manager.Models.Processings.Teams.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Teams
{
    public partial class TeamProcessingServiceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdWhenIdIsInvalidAndLogItAsync(int invalidId)
        {
            var invalidTeamProcessingException =
                new InvalidTeamProcessingException(message: "Team id is invalid.");

            var expectedTeamProcessingValidationException =
                new TeamProcessingValidationException(
                    message: "Team processing validation error occurred, fix the errors and try again.",
                    innerException: invalidTeamProcessingException);

            ValueTask<Team> retrieveByIdTask = this.teamProcessingService.RetrieveTeamByIdAsync(invalidId);

            TeamProcessingValidationException actualException =
                await Assert.ThrowsAsync<TeamProcessingValidationException>(retrieveByIdTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedTeamProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowValidationExceptionOnRetrieveMembersWhenIdIsInvalidAndLogItAsync(int invalidId)
        {
            var invalidTeamProcessingException =
                new InvalidTeamProcessingException(message: "Team id is invalid.");

            var expectedTeamProcessingValidationException =
                new TeamProcessingValidationException(
                    message: "Team processing validation error occurred, fix the errors and try again.",
                    innerException: invalidTeamProcessingException);

            ValueTask<IEnumerable<TeamMember>> retrieveMembersTask =
                this.teamProcessingService.RetrieveTeamMembersAsync(invalidId);

            TeamProcessingValidationException actualException =
                await Assert.ThrowsAsync<TeamProcessingValidationException>(retrieveMembersTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedTeamProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
