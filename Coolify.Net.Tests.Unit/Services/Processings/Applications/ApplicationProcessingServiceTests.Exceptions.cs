// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.Applications.Exceptions;
using Coolify.Net.Models.Processings.Applications.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Processings.Applications
{
    public partial class ApplicationProcessingServiceTests
    {
        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> FoundationValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ApplicationValidationException("test", inner),
                new ApplicationDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ApplicationDependencyException("test", inner),
                new ApplicationServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationProcessingService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationProcessingDependencyValidationException>(retrieveAllApplicationsTask.AsTask);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationProcessingService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationProcessingDependencyException>(retrieveAllApplicationsTask.AsTask);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.applicationServiceMock
                .Setup(service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Application>> retrieveAllApplicationsTask =
                this.applicationProcessingService.RetrieveAllApplicationsAsync();

            await Assert.ThrowsAsync<ApplicationProcessingServiceException>(retrieveAllApplicationsTask.AsTask);

            this.applicationServiceMock.Verify(
                service => service.RetrieveAllApplicationsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.applicationServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
