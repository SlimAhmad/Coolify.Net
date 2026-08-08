// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.Servers.Exceptions;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveDomainsWhenValidationErrorOccursAsync(
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
                .Setup(service => service.RetrieveServerDomainsAsync(someServerUuid))
                .ThrowsAsync(validationException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverClient.RetrieveServerDomainsAsync(someServerUuid);

            ServerClientValidationException actualException =
                await Assert.ThrowsAsync<ServerClientValidationException>(
                    retrieveServerDomainsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientValidationException);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerDomainsAsync(someServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveDomainsWhenDependencyOrServiceErrorOccursAsync(
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
                .Setup(service => service.RetrieveServerDomainsAsync(someServerUuid))
                .ThrowsAsync(dependencyOrServiceException);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverClient.RetrieveServerDomainsAsync(someServerUuid);

            ServerClientDependencyException actualException =
                await Assert.ThrowsAsync<ServerClientDependencyException>(
                    retrieveServerDomainsTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedServerClientDependencyException);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerDomainsAsync(someServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveDomainsWhenExceptionOccursAsync()
        {
            // given
            string someServerUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.serverServiceMock
                .Setup(service => service.RetrieveServerDomainsAsync(someServerUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<string>> retrieveServerDomainsTask =
                this.serverClient.RetrieveServerDomainsAsync(someServerUuid);

            // then
            await Assert.ThrowsAsync<ServerClientServiceException>(retrieveServerDomainsTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveServerDomainsAsync(someServerUuid), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
        }
    }
}
