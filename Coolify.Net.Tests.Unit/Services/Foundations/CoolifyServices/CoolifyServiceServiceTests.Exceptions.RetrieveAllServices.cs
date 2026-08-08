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
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            CoolifyServiceDependencyValidationException expectedCoolifyServiceDependencyValidationException =
                CreateInvalidCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            CoolifyServiceDependencyValidationException actualCoolifyServiceDependencyValidationException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveAllServicesTask.AsTask);

            // then
            actualCoolifyServiceDependencyValidationException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllIfConflictErrorOccursAndLogItAsync()
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            CoolifyServiceDependencyValidationException expectedCoolifyServiceDependencyValidationException =
                CreateAlreadyExistsCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            CoolifyServiceDependencyValidationException actualCoolifyServiceDependencyValidationException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(retrieveAllServicesTask.AsTask);

            // then
            actualCoolifyServiceDependencyValidationException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveAllIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            var httpRequestException = new HttpRequestException("Network failure.");

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(retrieveAllServicesTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedCoolifyServiceServiceException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceService.RetrieveAllServicesAsync();

            CoolifyServiceServiceException actualCoolifyServiceServiceException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(retrieveAllServicesTask.AsTask);

            // then
            actualCoolifyServiceServiceException.Should()
                .BeEquivalentTo(expectedCoolifyServiceServiceException);

            this.coolifyApiBrokerMock.Verify(
                broker => broker.GetAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
