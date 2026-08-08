// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveDomainsWhenServerUuidIsInvalidAndLogItAsync(
            string invalidServerUuid)
        {
            // given
            var invalidServerException =
                new InvalidServerException(
                    message: "Invalid server. Please fix the errors and try again.");

            invalidServerException.UpsertDataList(
                key: "serverUuid",
                value: "Text is required");

            var expectedServerValidationException =
                new ServerValidationException(
                    message: "Server validation error occurred, fix the errors and try again.",
                    innerException: invalidServerException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverService.RetrieveServerDomainsAsync(invalidServerUuid);

            ServerValidationException actualServerValidationException =
                await Assert.ThrowsAsync<ServerValidationException>(
                    retrieveServerDomainsTask.AsTask);

            // then
            actualServerValidationException.Should().BeEquivalentTo(expectedServerValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is<Exception>(exception =>
                    exception.Message == expectedServerValidationException.Message)),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
