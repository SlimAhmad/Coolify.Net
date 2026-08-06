// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenServerUuidIsInvalidAndLogItAsync(
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
            ValueTask removeServerTask =
                this.serverService.RemoveServerAsync(invalidServerUuid);

            ServerValidationException actualServerValidationException =
                await Assert.ThrowsAsync<ServerValidationException>(
                    removeServerTask.AsTask);

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
