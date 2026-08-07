// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Resource.Manager.Models.Foundations.Systems;
using Coolify.Resource.Manager.Models.Foundations.Systems.Exceptions;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Foundations.Systems
{
    public partial class SystemServiceTests
    {
        [Theory]
        [MemberData(nameof(DependencyValidationHttpStatusCodes))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveVersionWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemDependencyValidationException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(CriticalDependencyHttpStatusCodes))]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveVersionWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemDependencyException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveVersionWhenHttpErrorOccursAsync(
            HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemDependencyException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveVersionWhenHttpRequestExceptionHasNoStatusCodeAsync()
        {
            var httpRequestException = new HttpRequestException("Network failure.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemDependencyException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveVersionWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemServiceException>(retrieveVersionTask.AsTask);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(DependencyHttpStatusCodes))]
        public async Task ShouldThrowDependencyExceptionOnCheckHealthWhenHttpErrorOccursAsync(HttpStatusCode statusCode)
        {
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetHealthCheckAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            ValueTask<bool> checkHealthTask = this.systemService.CheckHealthAsync();

            await Assert.ThrowsAsync<SystemDependencyException>(checkHealthTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetHealthCheckAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnCheckHealthWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetHealthCheckAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<bool> checkHealthTask = this.systemService.CheckHealthAsync();

            await Assert.ThrowsAsync<SystemServiceException>(checkHealthTask.AsTask);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetHealthCheckAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
