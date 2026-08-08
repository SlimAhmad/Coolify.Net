// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems;
using Coolify.Net.Models.Foundations.Systems.Exceptions;
using Coolify.Net.Models.Processings.Systems.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Processings.Systems
{
    public partial class SystemProcessingServiceTests
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
                new SystemValidationException("test", inner),
                new SystemDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new SystemDependencyException("test", inner),
                new SystemServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveVersionWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemProcessingService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemProcessingDependencyValidationException>(retrieveVersionTask.AsTask);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveVersionWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemProcessingService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemProcessingDependencyException>(retrieveVersionTask.AsTask);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveVersionWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.systemServiceMock
                .Setup(service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<SystemInfo> retrieveVersionTask = this.systemProcessingService.RetrieveVersionAsync();

            await Assert.ThrowsAsync<SystemProcessingServiceException>(retrieveVersionTask.AsTask);

            this.systemServiceMock.Verify(
                service => service.RetrieveVersionAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.systemServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
