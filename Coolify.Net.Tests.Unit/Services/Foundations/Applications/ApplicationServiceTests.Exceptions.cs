// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Applications;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationDependencyValidationException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationServiceException>(retrieveAllApplicationsTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnAddPublicWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            Application someApplication = CreateRandomApplication();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<Application> addApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            await Assert.ThrowsAsync<ApplicationDependencyException>(addApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddPublicWhenExceptionOccursAsync()
        {
            Application someApplication = CreateRandomApplication();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(
                    It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<Application> addApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            await Assert.ThrowsAsync<ApplicationServiceException>(addApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask removeApplicationTask = this.applicationService.RemoveApplicationAsync(someApplicationUuid);

            await Assert.ThrowsAsync<ApplicationDependencyException>(removeApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask removeApplicationTask = this.applicationService.RemoveApplicationAsync(someApplicationUuid);

            await Assert.ThrowsAsync<ApplicationServiceException>(removeApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteApplicationAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnStartWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationStartAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask startApplicationTask = this.applicationService.StartApplicationAsync(someApplicationUuid);

            await Assert.ThrowsAsync<ApplicationDependencyException>(startApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationStartAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnStartWhenExceptionOccursAsync()
        {
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostApplicationStartAsync(someApplicationUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask startApplicationTask = this.applicationService.StartApplicationAsync(someApplicationUuid);

            await Assert.ThrowsAsync<ApplicationServiceException>(startApplicationTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostApplicationStartAsync(someApplicationUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
