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
        public async Task ShouldThrowClientValidationExceptionOnRetrieveValidationWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            // given
            string someServerUuid = GetRandomString();

            var expectedServerClientValidationException =
                new ServerClientValidationException(
                    message: "Server client validation error occurred, fix the errors and try again.",
                    innerException: validationException.InnerException as Xeption,
                    data: (validationException.InnerException as Xeption).Data);

            this.serverServiceMock
                .Setup(service => service.RetrieveServerValidationAsync(someServerUuid))
                .ThrowsAsync(validationException);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverClient.RetrieveServerValidationAsync(someServerUuid);

            ServerClientValidationException actualException =
                await Assert.ThrowsAsync<ServerClientValidationException>(
                    retrieveServerValidationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientValidationException);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerValidationAsync(someServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveValidationWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            // given
            string someServerUuid = GetRandomString();

            var expectedServerClientDependencyException =
                new ServerClientDependencyException(
                    message: "Server client dependency error occurred, contact support.",
                    innerException: dependencyOrServiceException.InnerException as Xeption,
                    data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.serverServiceMock
                .Setup(service => service.RetrieveServerValidationAsync(someServerUuid))
                .ThrowsAsync(dependencyOrServiceException);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverClient.RetrieveServerValidationAsync(someServerUuid);

            ServerClientDependencyException actualException =
                await Assert.ThrowsAsync<ServerClientDependencyException>(
                    retrieveServerValidationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientDependencyException);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerValidationAsync(someServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveValidationWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.serverServiceMock
                .Setup(service => service.RetrieveServerValidationAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Server> retrieveServerValidationTask =
                this.serverClient.RetrieveServerValidationAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerClientServiceException>(retrieveServerValidationTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerValidationAsync(someServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
