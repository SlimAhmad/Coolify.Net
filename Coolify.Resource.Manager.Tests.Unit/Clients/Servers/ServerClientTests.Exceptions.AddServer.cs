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
        public async Task ShouldThrowClientValidationExceptionOnAddWhenValidationErrorOccursAsync(
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
                .Setup(service => service.AddServerAsync(someServer))
                .ThrowsAsync(validationException);

            // when
            ValueTask<Server> addServerTask =
                this.serverClient.AddServerAsync(someServer);

            ServerClientValidationException actualException =
                await Assert.ThrowsAsync<ServerClientValidationException>(
                    addServerTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientValidationException);

            this.serverServiceMock.Verify(
                service => service.AddServerAsync(someServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnAddWhenDependencyOrServiceErrorOccursAsync(
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
                .Setup(service => service.AddServerAsync(someServer))
                .ThrowsAsync(dependencyOrServiceException);

            // when
            ValueTask<Server> addServerTask =
                this.serverClient.AddServerAsync(someServer);

            ServerClientDependencyException actualException =
                await Assert.ThrowsAsync<ServerClientDependencyException>(
                    addServerTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientDependencyException);

            this.serverServiceMock.Verify(
                service => service.AddServerAsync(someServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            // given
            Server someServer = CreateRandomServer();
            var exception = new Exception("Unexpected error.");

            this.serverServiceMock
                .Setup(service => service.AddServerAsync(someServer))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> addServerTask =
                this.serverClient.AddServerAsync(someServer);

            // then
            await Assert.ThrowsAsync<ServerClientServiceException>(addServerTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.AddServerAsync(someServer), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
