// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Servers.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnModifyWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            Server someServer = CreateRandomServer();

            var expectedServerClientValidationException =
                new ServerClientValidationException(
                    message: "Server client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: (validationException.InnerException as Xeption).Data);

            this.serverServiceMock
                .Setup(service => service.ModifyServerAsync(someServer))
                .ThrowsAsync(validationException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverClient.ModifyServerAsync(someServer);

            ServerClientValidationException actualException =
                await Assert.ThrowsAsync<ServerClientValidationException>(
                    modifyServerTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientValidationException);

            this.serverServiceMock.Verify(
                service => service.ModifyServerAsync(someServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnModifyWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            // given
            Server someServer = CreateRandomServer();

            var expectedServerClientDependencyException =
                new ServerClientDependencyException(
                    message: "Server client dependency error occurred, contact support.",
                    innerException: dependencyOrServiceException.InnerException as Xeption,
                    data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.serverServiceMock
                .Setup(service => service.ModifyServerAsync(someServer))
                .ThrowsAsync(dependencyOrServiceException);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverClient.ModifyServerAsync(someServer);

            ServerClientDependencyException actualException =
                await Assert.ThrowsAsync<ServerClientDependencyException>(
                    modifyServerTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientDependencyException);

            this.serverServiceMock.Verify(
                service => service.ModifyServerAsync(someServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnModifyWhenExceptionOccursAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var exception = new Exception("Unexpected error.");

            this.serverServiceMock
                .Setup(service => service.ModifyServerAsync(someServer))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> modifyServerTask =
                this.serverClient.ModifyServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerClientServiceException>(modifyServerTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.ModifyServerAsync(someServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
