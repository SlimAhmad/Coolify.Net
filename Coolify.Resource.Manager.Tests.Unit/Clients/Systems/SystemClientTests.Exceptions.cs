// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Systems.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Systems;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Systems
{
    public partial class SystemClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveVersionWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new SystemClientValidationException(
                message: "System client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<SystemInfo> task = this.systemClient.RetrieveVersionAsync();

            SystemClientValidationException actual =
                await Assert.ThrowsAsync<SystemClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveVersionWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new SystemClientDependencyException(
                message: "System client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<SystemInfo> task = this.systemClient.RetrieveVersionAsync();

            SystemClientDependencyException actual =
                await Assert.ThrowsAsync<SystemClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveVersionWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<SystemInfo> task = this.systemClient.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemClientServiceException>(task.AsTask);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveVersionAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<SystemInfo> task = this.systemClient.RetrieveVersionAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnCheckHealthWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new SystemClientValidationException(
                message: "System client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.systemServiceMock
                .Setup(service => service.CheckHealthAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<bool> task = this.systemClient.CheckHealthAsync();

            SystemClientValidationException actual =
                await Assert.ThrowsAsync<SystemClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.systemServiceMock.Verify(
                service => service.CheckHealthAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnCheckHealthWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.systemServiceMock
                .Setup(service => service.CheckHealthAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<bool> task = this.systemClient.CheckHealthAsync();

            await Assert.ThrowsAsync<SystemClientServiceException>(task.AsTask);

            this.systemServiceMock.Verify(
                service => service.CheckHealthAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
        }
    }
}
