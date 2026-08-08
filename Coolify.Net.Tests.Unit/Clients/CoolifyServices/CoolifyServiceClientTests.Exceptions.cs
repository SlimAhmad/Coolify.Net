// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Clients.CoolifyServices.Exceptions;
using Coolify.Net.Models.Foundations.CoolifyServices;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.CoolifyServices
{
    public partial class CoolifyServiceClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new CoolifyServiceClientValidationException(
                message: "Service client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<CoolifyService>> task = this.coolifyServiceClient.RetrieveAllServicesAsync();

            CoolifyServiceClientValidationException actual =
                await Assert.ThrowsAsync<CoolifyServiceClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new CoolifyServiceClientDependencyException(
                message: "Service client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<CoolifyService>> task = this.coolifyServiceClient.RetrieveAllServicesAsync();

            CoolifyServiceClientDependencyException actual =
                await Assert.ThrowsAsync<CoolifyServiceClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<CoolifyService>> task = this.coolifyServiceClient.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceClientServiceException>(task.AsTask);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<CoolifyService>> task = this.coolifyServiceClient.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnAddWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            CoolifyService someCoolifyService = CreateRandomCoolifyService();

            var expected = new CoolifyServiceClientValidationException(
                message: "Service client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.coolifyServiceServiceMock
                .Setup(service => service.AddCoolifyServiceAsync(someCoolifyService, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<CoolifyService> task = this.coolifyServiceClient.AddCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceClientValidationException actual =
                await Assert.ThrowsAsync<CoolifyServiceClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.coolifyServiceServiceMock.Verify(service =>
                service.AddCoolifyServiceAsync(someCoolifyService, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            var exception = new Exception("Unexpected error.");

            this.coolifyServiceServiceMock
                .Setup(service => service.AddCoolifyServiceAsync(someCoolifyService, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<CoolifyService> task = this.coolifyServiceClient.AddCoolifyServiceAsync(someCoolifyService);

            await Assert.ThrowsAsync<CoolifyServiceClientServiceException>(task.AsTask);

            this.coolifyServiceServiceMock.Verify(service =>
                service.AddCoolifyServiceAsync(someCoolifyService, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRemoveWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someServiceUuid = GetRandomString();

            var expected = new CoolifyServiceClientValidationException(
                message: "Service client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.coolifyServiceServiceMock
                .Setup(service => service.RemoveCoolifyServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.coolifyServiceClient.RemoveCoolifyServiceAsync(someServiceUuid);

            CoolifyServiceClientValidationException actual =
                await Assert.ThrowsAsync<CoolifyServiceClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RemoveCoolifyServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyServiceServiceMock
                .Setup(service => service.RemoveCoolifyServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.coolifyServiceClient.RemoveCoolifyServiceAsync(someServiceUuid);

            await Assert.ThrowsAsync<CoolifyServiceClientServiceException>(task.AsTask);

            this.coolifyServiceServiceMock.Verify(service =>
                service.RemoveCoolifyServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnStartWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someServiceUuid = GetRandomString();

            var expected = new CoolifyServiceClientValidationException(
                message: "Service client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.coolifyServiceServiceMock
                .Setup(service => service.StartServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.coolifyServiceClient.StartServiceAsync(someServiceUuid);

            CoolifyServiceClientValidationException actual =
                await Assert.ThrowsAsync<CoolifyServiceClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.coolifyServiceServiceMock.Verify(service =>
                service.StartServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnStartWhenExceptionOccursAsync()
        {
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyServiceServiceMock
                .Setup(service => service.StartServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.coolifyServiceClient.StartServiceAsync(someServiceUuid);

            await Assert.ThrowsAsync<CoolifyServiceClientServiceException>(task.AsTask);

            this.coolifyServiceServiceMock.Verify(service =>
                service.StartServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
