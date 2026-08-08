// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Servers;
using Coolify.Net.Models.Foundations.Servers.Exceptions;
using Coolify.Net.Models.Processings.Servers.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Processings.Servers
{
    public partial class ServerProcessingServiceTests
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
                new ServerValidationException("test", inner),
                new ServerDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ServerDependencyException("test", inner),
                new ServerServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverProcessingService.RetrieveAllServersAsync();

            await Assert.ThrowsAsync<ServerProcessingDependencyValidationException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverProcessingService.RetrieveAllServersAsync();

            await Assert.ThrowsAsync<ServerProcessingDependencyException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.serverServiceMock
                .Setup(service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Server>> retrieveAllServersTask =
                this.serverProcessingService.RetrieveAllServersAsync();

            await Assert.ThrowsAsync<ServerProcessingServiceException>(retrieveAllServersTask.AsTask);

            this.serverServiceMock.Verify(
                service => service.RetrieveAllServersAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.serverServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
