// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            CoolifyServiceDependencyValidationException expectedCoolifyServiceDependencyValidationException =
                CreateInvalidCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> retrieveServiceByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(someServiceUuid);

            CoolifyServiceDependencyValidationException actualCoolifyServiceDependencyValidationException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveServiceByUuidTask.AsTask);

            // then
            actualCoolifyServiceDependencyValidationException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            CoolifyServiceDependencyValidationException expectedCoolifyServiceDependencyValidationException =
                CreateAlreadyExistsCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> retrieveServiceByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(someServiceUuid);

            CoolifyServiceDependencyValidationException actualCoolifyServiceDependencyValidationException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveServiceByUuidTask.AsTask);

            // then
            actualCoolifyServiceDependencyValidationException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> retrieveServiceByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(someServiceUuid);

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveServiceByUuidTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> retrieveServiceByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(someServiceUuid);

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveServiceByUuidTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> retrieveServiceByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(someServiceUuid);

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveServiceByUuidTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedCoolifyServiceServiceException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<CoolifyService> retrieveServiceByUuidTask =
                this.coolifyServiceService.RetrieveServiceByUuidAsync(someServiceUuid);

            CoolifyServiceServiceException actualCoolifyServiceServiceException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(retrieveServiceByUuidTask.AsTask);

            // then
            actualCoolifyServiceServiceException.Should()
                .BeEquivalentTo(expectedCoolifyServiceServiceException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetServiceByUuidAsync(someServiceUuid, It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
