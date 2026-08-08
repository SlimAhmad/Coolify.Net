// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceServiceException>(retrieveAllServicesTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnAddWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceService.AddCoolifyServiceAsync(someCoolifyService);

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(addCoolifyServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddWhenExceptionOccursAsync()
        {
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<CoolifyService> addCoolifyServiceTask =
                this.coolifyServiceService.AddCoolifyServiceAsync(someCoolifyService);

            await Assert.ThrowsAsync<CoolifyServiceServiceException>(addCoolifyServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
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
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask removeCoolifyServiceTask = this.coolifyServiceService.RemoveCoolifyServiceAsync(someServiceUuid);

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(removeCoolifyServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveWhenExceptionOccursAsync()
        {
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.DeleteServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask removeCoolifyServiceTask = this.coolifyServiceService.RemoveCoolifyServiceAsync(someServiceUuid);

            await Assert.ThrowsAsync<CoolifyServiceServiceException>(removeCoolifyServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.DeleteServiceAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

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
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceStartAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask startServiceTask = this.coolifyServiceService.StartServiceAsync(someServiceUuid);

            await Assert.ThrowsAsync<CoolifyServiceDependencyException>(startServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceStartAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnStartWhenExceptionOccursAsync()
        {
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceStartAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask startServiceTask = this.coolifyServiceService.StartServiceAsync(someServiceUuid);

            await Assert.ThrowsAsync<CoolifyServiceServiceException>(startServiceTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceStartAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
