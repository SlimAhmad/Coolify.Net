// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Externals.Applications;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Applications
{
    public partial class ApplicationServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddPublicApplicationIfBadRequestErrorOccursAndLogItAsync()
        {
            // given
            Application someApplication = CreateRandomApplication();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.BadRequest);

            ApplicationDependencyValidationException expectedException =
                CreateInvalidApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> addPublicApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(addPublicApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddPublicApplicationIfConflictErrorOccursAndLogItAsync()
        {
            // given
            Application someApplication = CreateRandomApplication();
            HttpRequestException httpRequestException = CreateHttpRequestException(HttpStatusCode.Conflict);

            ApplicationDependencyValidationException expectedException =
                CreateAlreadyExistsApplicationDependencyValidationException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> addPublicApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            ApplicationDependencyValidationException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyValidationException>(addPublicApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddPublicApplicationIfCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            Application someApplication = CreateRandomApplication();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> addPublicApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(addPublicApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public async Task ShouldThrowDependencyExceptionOnAddPublicApplicationIfNonCriticalErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            Application someApplication = CreateRandomApplication();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> addPublicApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(addPublicApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddPublicApplicationIfHttpRequestExceptionHasNoStatusCodeAndLogItAsync()
        {
            // given
            Application someApplication = CreateRandomApplication();
            var httpRequestException = new HttpRequestException("Network failure.");

            ApplicationDependencyException expectedException =
                CreateFailedApplicationDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask<Application> addPublicApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            ApplicationDependencyException actualException =
                await Assert.ThrowsAsync<ApplicationDependencyException>(addPublicApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAddPublicApplicationIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Application someApplication = CreateRandomApplication();
            var exception = new Exception("Unexpected error.");

            ApplicationServiceException expectedException =
                CreateFailedApplicationServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()))
                .ThrowsAsync(exception);

            // when
            ValueTask<Application> addPublicApplicationTask =
                this.applicationService.AddPublicApplicationAsync(someApplication);

            ApplicationServiceException actualException =
                await Assert.ThrowsAsync<ApplicationServiceException>(addPublicApplicationTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostPublicApplicationAsync(It.IsAny<ExternalApplication>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
