// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Processings.Servers.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Servers
{
    public partial class ServerProcessingServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddWhenServerIsNullAndLogItAsync()
        {
            Server nullServer = null;

            var nullServerProcessingException =
                new NullServerProcessingException(message: "Server is null.");

            var expectedServerProcessingValidationException =
                new ServerProcessingValidationException(
                    message: "Server processing validation error occurred, fix the errors and try again.",
                    innerException: nullServerProcessingException);

            ValueTask<Server> addServerTask = this.serverProcessingService.AddServerAsync(nullServer);

            ServerProcessingValidationException actualException =
                await Assert.ThrowsAsync<ServerProcessingValidationException>(addServerTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedServerProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyWhenServerIsNullAndLogItAsync()
        {
            Server nullServer = null;

            var nullServerProcessingException =
                new NullServerProcessingException(message: "Server is null.");

            var expectedServerProcessingValidationException =
                new ServerProcessingValidationException(
                    message: "Server processing validation error occurred, fix the errors and try again.",
                    innerException: nullServerProcessingException);

            ValueTask<Server> modifyServerTask = this.serverProcessingService.ModifyServerAsync(nullServer);

            ServerProcessingValidationException actualException =
                await Assert.ThrowsAsync<ServerProcessingValidationException>(modifyServerTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedServerProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRetrieveByUuidWhenServerUuidIsInvalidAndLogItAsync(
            string invalidServerUuid)
        {
            var invalidServerProcessingException =
                new InvalidServerProcessingException(message: "Server uuid is invalid.");

            var expectedServerProcessingValidationException =
                new ServerProcessingValidationException(
                    message: "Server processing validation error occurred, fix the errors and try again.",
                    innerException: invalidServerProcessingException);

            ValueTask<Server> retrieveByUuidTask =
                this.serverProcessingService.RetrieveServerByUuidAsync(invalidServerUuid);

            ServerProcessingValidationException actualException =
                await Assert.ThrowsAsync<ServerProcessingValidationException>(retrieveByUuidTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedServerProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public async Task ShouldThrowValidationExceptionOnRemoveWhenServerUuidIsInvalidAndLogItAsync(
            string invalidServerUuid)
        {
            var invalidServerProcessingException =
                new InvalidServerProcessingException(message: "Server uuid is invalid.");

            var expectedServerProcessingValidationException =
                new ServerProcessingValidationException(
                    message: "Server processing validation error occurred, fix the errors and try again.",
                    innerException: invalidServerProcessingException);

            ValueTask removeServerTask = this.serverProcessingService.RemoveServerAsync(invalidServerUuid);

            ServerProcessingValidationException actualException =
                await Assert.ThrowsAsync<ServerProcessingValidationException>(removeServerTask.AsTask);

            actualException.Should().BeEquivalentTo(expectedServerProcessingValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
