// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            CoolifyServiceDependencyValidationException expectedCoolifyServiceDependencyValidationException =
                CreateInvalidCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> modifyCoolifyServiceTask =
                this.coolifyServiceService.ModifyCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceDependencyValidationException actualCoolifyServiceDependencyValidationException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(modifyCoolifyServiceTask.AsTask);

            // then
            actualCoolifyServiceDependencyValidationException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyValidationException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfConflictErrorOccursAndLogItAsync()
        {
            // given
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            CoolifyServiceDependencyValidationException expectedCoolifyServiceDependencyValidationException =
                CreateAlreadyExistsCoolifyServiceDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> modifyCoolifyServiceTask =
                this.coolifyServiceService.ModifyCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceDependencyValidationException actualCoolifyServiceDependencyValidationException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyValidationException>(modifyCoolifyServiceTask.AsTask);

            // then
            actualCoolifyServiceDependencyValidationException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyValidationException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                    Times.Once);

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
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> modifyCoolifyServiceTask =
                this.coolifyServiceService.ModifyCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyCoolifyServiceTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                    Times.Once);

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
        public async Task ShouldThrowDependencyExceptionOnModifyIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> modifyCoolifyServiceTask =
                this.coolifyServiceService.ModifyCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyCoolifyServiceTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            var httpRequestException = new HttpRequestException("Network failure.");

            CoolifyServiceDependencyException expectedCoolifyServiceDependencyException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<CoolifyService> modifyCoolifyServiceTask =
                this.coolifyServiceService.ModifyCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceDependencyException actualCoolifyServiceDependencyException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(modifyCoolifyServiceTask.AsTask);

            // then
            actualCoolifyServiceDependencyException.Should()
                .BeEquivalentTo(expectedCoolifyServiceDependencyException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedCoolifyServiceDependencyException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfServiceErrorOccursAndLogItAsync()
        {
            // given
            CoolifyService someCoolifyService = CreateRandomCoolifyService();
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedCoolifyServiceServiceException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PatchServiceAsync(
                    It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<CoolifyService> modifyCoolifyServiceTask =
                this.coolifyServiceService.ModifyCoolifyServiceAsync(someCoolifyService);

            CoolifyServiceServiceException actualCoolifyServiceServiceException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(modifyCoolifyServiceTask.AsTask);

            // then
            actualCoolifyServiceServiceException.Should()
                .BeEquivalentTo(expectedCoolifyServiceServiceException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PatchServiceAsync(It.IsAny<ExternalCoolifyService>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedCoolifyServiceServiceException))),
                    Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
