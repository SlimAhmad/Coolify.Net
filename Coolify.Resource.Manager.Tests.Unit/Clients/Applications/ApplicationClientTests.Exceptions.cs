// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Clients.Applications.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using FluentAssertions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Applications
{
    public partial class ApplicationClientTests
    {
        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRetrieveAllWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            var expected = new ApplicationClientValidationException(
                message: "Application client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<IEnumerable<Application>> task = this.applicationClient.RetrieveAllApplicationsAsync();

            ApplicationClientValidationException actual =
                await Assert.ThrowsAsync<ApplicationClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyAndServiceExceptions))]
        public async Task ShouldThrowClientDependencyExceptionOnRetrieveAllWhenDependencyOrServiceErrorOccursAsync(
            Xeption dependencyOrServiceException)
        {
            var expected = new ApplicationClientDependencyException(
                message: "Application client dependency error occurred, contact support.",
                innerException: dependencyOrServiceException.InnerException as Xeption,
                data: (dependencyOrServiceException.InnerException as Xeption).Data);

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(dependencyOrServiceException);

            ValueTask<IEnumerable<Application>> task = this.applicationClient.RetrieveAllApplicationsAsync();

            ApplicationClientDependencyException actual =
                await Assert.ThrowsAsync<ApplicationClientDependencyException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Application>> task = this.applicationClient.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationClientServiceException>(task.AsTask);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotWrapOperationCanceledExceptionOnRetrieveAllAsync()
        {
            var operationCanceledException = new OperationCanceledException();

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(operationCanceledException);

            ValueTask<IEnumerable<Application>> task = this.applicationClient.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(task.AsTask);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnAddPublicWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            Application someApplication = CreateRandomApplication();

            var expected = new ApplicationClientValidationException(
                message: "Application client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.applicationServiceMock
                .Setup(service => service.AddPublicApplicationAsync(someApplication, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask<Application> task = this.applicationClient.AddPublicApplicationAsync(someApplication);

            ApplicationClientValidationException actual =
                await Assert.ThrowsAsync<ApplicationClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.applicationServiceMock.Verify(service =>
                service.AddPublicApplicationAsync(someApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnAddPublicWhenExceptionOccursAsync()
        {
            Application someApplication = CreateRandomApplication();
            var exception = new Exception("Unexpected error.");

            this.applicationServiceMock
                .Setup(service => service.AddPublicApplicationAsync(someApplication, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Application> task = this.applicationClient.AddPublicApplicationAsync(someApplication);

            await Assert.ThrowsAsync<ApplicationClientServiceException>(task.AsTask);

            this.applicationServiceMock.Verify(service =>
                service.AddPublicApplicationAsync(someApplication, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnRemoveWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someApplicationUuid = GetRandomString();

            var expected = new ApplicationClientValidationException(
                message: "Application client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.applicationServiceMock
                .Setup(service => service.RemoveApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.applicationClient.RemoveApplicationAsync(someApplicationUuid);

            ApplicationClientValidationException actual =
                await Assert.ThrowsAsync<ApplicationClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.applicationServiceMock.Verify(service =>
                service.RemoveApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.applicationServiceMock
                .Setup(service => service.RemoveApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.applicationClient.RemoveApplicationAsync(someApplicationUuid);

            await Assert.ThrowsAsync<ApplicationClientServiceException>(task.AsTask);

            this.applicationServiceMock.Verify(service =>
                service.RemoveApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(ValidationExceptions))]
        public async Task ShouldThrowClientValidationExceptionOnStartWhenValidationErrorOccursAsync(
            Xeption validationException)
        {
            string someApplicationUuid = GetRandomString();

            var expected = new ApplicationClientValidationException(
                message: "Application client validation error occurred, fix the errors and try again.",
                innerException: validationException.InnerException as Xeption,
                data: (validationException.InnerException as Xeption).Data);

            this.applicationServiceMock
                .Setup(service => service.StartApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(validationException);

            ValueTask task = this.applicationClient.StartApplicationAsync(someApplicationUuid);

            ApplicationClientValidationException actual =
                await Assert.ThrowsAsync<ApplicationClientValidationException>(task.AsTask);

            actual.Should().BeEquivalentTo(expected);

            this.applicationServiceMock.Verify(service =>
                service.StartApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowClientServiceExceptionOnStartWhenExceptionOccursAsync()
        {
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.applicationServiceMock
                .Setup(service => service.StartApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask task = this.applicationClient.StartApplicationAsync(someApplicationUuid);

            await Assert.ThrowsAsync<ApplicationClientServiceException>(task.AsTask);

            this.applicationServiceMock.Verify(service =>
                service.StartApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
        }
    }
}
