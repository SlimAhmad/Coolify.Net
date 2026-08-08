// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Servers.Exceptions;
using Coolify.Net.Models.Foundations.Servers;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            var expectedServerClientValidationException =
                new ServerClientValidationException(
                    message: "Server client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: (validationException.InnerException as Xeption).Data);

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync())
                .ThrowsAsync(validationException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverClient.RetrieveAllServersAsync();

            ServerClientValidationException actualException =
                await Assert.ThrowsAsync<ServerClientValidationException>(
                    retrieveAllServersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientValidationException);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            // given
            var expectedServerClientDependencyException =
                new ServerClientDependencyException(
                    message: "Server client dependency error occurred, contact support.",
                    innerException: dependencyOrServiceException.InnerException as Xeption,
                    data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync())
                .ThrowsAsync(dependencyOrServiceException);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverClient.RetrieveAllServersAsync();

            ServerClientDependencyException actualException =
                await Assert.ThrowsAsync<ServerClientDependencyException>(
                    retrieveAllServersTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientDependencyException);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            // given
            var exception = new Exception("Unexpected error.");

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync())
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverClient.RetrieveAllServersAsync();

            // then
            await Assert.ThrowsAsync<ServerClientServiceException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
