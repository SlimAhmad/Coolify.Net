// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Teams.Exceptions;
using Coolify.Net.Models.Foundations.Teams;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Teams
{
    public partial class TeamClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new TeamClientValidationException(
                message: "Team client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<Team>> task = this.teamClient.RetrieveAllTeamsAsync();

            TeamClientValidationException actual =
                await Assert.ThrowsAsync<TeamClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new TeamClientDependencyException(
                message: "Team client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<Team>> task = this.teamClient.RetrieveAllTeamsAsync();

            TeamClientDependencyException actual =
                await Assert.ThrowsAsync<TeamClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Team>> task = this.teamClient.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamClientServiceException>(task.AsTask);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<Team>> task = this.teamClient.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveByIdWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            int someId = GetRandomId();

            var expected = new TeamClientValidationException(
                message: "Team client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.teamServiceMock
                .Setup(service => service.RetrieveTeamByIdAsync(someId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Team> task = this.teamClient.RetrieveTeamByIdAsync(someId);

            TeamClientValidationException actual =
                await Assert.ThrowsAsync<TeamClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.teamServiceMock.Verify(service =>
                service.RetrieveTeamByIdAsync(someId, It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveByIdWhenExceptionOccursAsync()
        {
            int someId = GetRandomId();
            var exception = new Exception("Unexpected error.");

            this.teamServiceMock
                .Setup(service => service.RetrieveTeamByIdAsync(someId, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Team> task = this.teamClient.RetrieveTeamByIdAsync(someId);

            await Assert.ThrowsAsync<TeamClientServiceException>(task.AsTask);

            this.teamServiceMock.Verify(service =>
                service.RetrieveTeamByIdAsync(someId, It.IsAny<CancellationToken>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
        }
    }
}
