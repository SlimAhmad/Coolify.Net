// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Teams;
using Coolify.Resource.Manager.Models.Foundations.Teams.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdWhenIdIsInvalidAndLogItAsync(int invalidId)
        {
            var invalidTeamException =
                new InvalidTeamException(
                    message: "Invalid team. Please fix the errors and try again.");

            invalidTeamException.UpsertDataList(key: "id", value: "Id is required");

            var expectedTeamValidationException =
                new TeamValidationException(
                    message: "Team validation error occurred, fix the errors and try again.",
                    innerException: invalidTeamException);

            ValueTask<Team> retrieveByIdTask = this.teamService.RetrieveTeamByIdAsync(invalidId);

            TeamValidationException actualException =
                await Assert.ThrowsAsync<TeamValidationException>(retrieveByIdTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedTeamValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldThrowValidationExceptionOnRetrieveMembersWhenIdIsInvalidAndLogItAsync(int invalidId)
        {
            var invalidTeamException =
                new InvalidTeamException(
                    message: "Invalid team. Please fix the errors and try again.");

            invalidTeamException.UpsertDataList(key: "id", value: "Id is required");

            var expectedTeamValidationException =
                new TeamValidationException(
                    message: "Team validation error occurred, fix the errors and try again.",
                    innerException: invalidTeamException);

            ValueTask<IEnumerable<TeamMember>> retrieveMembersTask =
                this.teamService.RetrieveTeamMembersAsync(invalidId);

            TeamValidationException actualException =
                await Assert.ThrowsAsync<TeamValidationException>(retrieveMembersTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedTeamValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
