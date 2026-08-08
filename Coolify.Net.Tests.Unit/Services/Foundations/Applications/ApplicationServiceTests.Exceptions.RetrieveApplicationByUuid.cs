// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveApplicationByUuidIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ApplicationDependencyValidationException expectedException =
                CreateInvalidApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> retrieveApplicationByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(someApplicationUuid);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(retrieveApplicationByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveApplicationByUuidIfConflictErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ApplicationDependencyValidationException expectedException =
                CreateAlreadyExistsApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> retrieveApplicationByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(someApplicationUuid);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(retrieveApplicationByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveApplicationByUuidIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> retrieveApplicationByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveApplicationByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnRetrieveApplicationByUuidIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someApplicationUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> retrieveApplicationByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveApplicationByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveApplicationByUuidIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var httpRequestException = new HttpRequestException("Network failure.");

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(someApplicationUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> retrieveApplicationByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(someApplicationUuid);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(retrieveApplicationByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveApplicationByUuidIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someApplicationUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.GetApplicationByUuidAsync(someApplicationUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask<Application> retrieveApplicationByUuidTask =
                this.applicationService.RetrieveApplicationByUuidAsync(someApplicationUuid);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(retrieveApplicationByUuidTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.GetApplicationByUuidAsync(someApplicationUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
