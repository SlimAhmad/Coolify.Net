// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Exceptions;
using Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.CoolifyServices
{
    public partial class CoolifyServiceProcessingServiceTests
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
                new CoolifyServiceValidationException("test", inner),
                new CoolifyServiceDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new CoolifyServiceDependencyException("test", inner),
                new CoolifyServiceServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceProcessingService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceProcessingDependencyValidationException>(retrieveAllServicesTask.AsTask);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceProcessingService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceProcessingDependencyException>(retrieveAllServicesTask.AsTask);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.coolifyServiceServiceMock
                .Setup(service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<CoolifyService>> retrieveAllServicesTask =
                this.coolifyServiceProcessingService.RetrieveAllServicesAsync();

            await Assert.ThrowsAsync<CoolifyServiceProcessingServiceException>(retrieveAllServicesTask.AsTask);

            this.coolifyServiceServiceMock.Verify(
                service => service.RetrieveAllServicesAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.coolifyServiceServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
