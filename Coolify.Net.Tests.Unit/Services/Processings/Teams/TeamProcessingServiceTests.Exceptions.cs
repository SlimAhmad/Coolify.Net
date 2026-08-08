// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Models.Foundations.Teams.Exceptions;
using Coolify.Net.Models.Processings.Teams.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Processings.Teams
{
    public partial class TeamProcessingServiceTests
    {
        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> FoundationValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new TeamValidationException("test", inner),
                new TeamDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new TeamDependencyException("test", inner),
                new TeamServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamProcessingService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamProcessingDependencyValidationException>(retrieveAllTeamsTask.AsTask);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamProcessingService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamProcessingDependencyException>(retrieveAllTeamsTask.AsTask);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.teamServiceMock
                .Setup(service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Team>> retrieveAllTeamsTask = this.teamProcessingService.RetrieveAllTeamsAsync();

            await Assert.ThrowsAsync<TeamProcessingServiceException>(retrieveAllTeamsTask.AsTask);

            this.teamServiceMock.Verify(
                service => service.RetrieveAllTeamsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.teamServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
