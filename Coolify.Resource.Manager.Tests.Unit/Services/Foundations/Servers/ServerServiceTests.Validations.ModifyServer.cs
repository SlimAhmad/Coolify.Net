// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyWhenServerIsNullAndLogItAsync()
        {
            // given
            Server nullServer = null;

            var nullServerException =
                new NullServerException(message: "Server is null.");

            var expectedServerValidationException =
                new ServerValidationException(
                    message: "Server validation error occurred, fix the errors and try again.",
                    innerException: nullServerException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(nullServer);

            ServerValidationException actualServerValidationException =
                await Assert.ThrowsAsync<ServerValidationException>(
                    modifyServerTask.AsTask);

            // then
            actualServerValidationException.Should().BeEquivalentTo(expectedServerValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is<Exception>(exception =>
                    exception.Message == expectedServerValidationException.Message)),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnModifyWhenServerIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidServer = new Server
            {
                Name = invalidText,
                Ip = invalidText,
                User = invalidText
            };

            var invalidServerException =
                new InvalidServerException(
                    message: "Invalid server. Please fix the errors and try again.");

            invalidServerException.UpsertDataList(key: nameof(Server.Name), value: "Text is required");
            invalidServerException.UpsertDataList(key: nameof(Server.Ip), value: "Text is required");
            invalidServerException.UpsertDataList(key: nameof(Server.User), value: "Text is required");

            var expectedServerValidationException =
                new ServerValidationException(
                    message: "Server validation error occurred, fix the errors and try again.",
                    innerException: invalidServerException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverService.ModifyServerAsync(invalidServer);

            ServerValidationException actualServerValidationException =
                await Assert.ThrowsAsync<ServerValidationException>(
                    modifyServerTask.AsTask);

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
