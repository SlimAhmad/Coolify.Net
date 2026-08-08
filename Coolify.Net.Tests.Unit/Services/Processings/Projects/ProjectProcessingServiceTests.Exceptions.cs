// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Models.Foundations.Projects.Exceptions;
using Coolify.Net.Models.Processings.Projects.Exceptions;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Services.Processings.Projects
{
    public partial class ProjectProcessingServiceTests
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
                new ProjectValidationException("test", inner),
                new ProjectDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> FoundationDependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ProjectDependencyException("test", inner),
                new ProjectServiceException("test", inner)
            };
        }

        [Theory]
        [MemberData(nameof(FoundationValidationExceptions))]
        public async Task ShouldThrowDependencyValidationExceptionOnRetrieveAllWhenFoundationValidationErrorOccursAsync(
            Xeption foundationValidationException)
        {
            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationValidationException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectProcessingService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectProcessingDependencyValidationException>(retrieveAllProjectsTask.AsTask);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [MemberData(nameof(FoundationDependencyAndServiceExceptions))]
        public async Task ShouldThrowDependencyExceptionOnRetrieveAllWhenFoundationDependencyOrServiceErrorOccursAsync(
            Xeption foundationException)
        {
            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(foundationException);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectProcessingService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectProcessingDependencyException>(retrieveAllProjectsTask.AsTask);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveAllWhenExceptionOccursAsync()
        {
            var exception = new Exception("Unexpected error.");

            this.projectServiceMock
                .Setup(service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            ValueTask<IEnumerable<Project>> retrieveAllProjectsTask =
                this.projectProcessingService.RetrieveAllProjectsAsync();

            await Assert.ThrowsAsync<ProjectProcessingServiceException>(retrieveAllProjectsTask.AsTask);

            this.projectServiceMock.Verify(
                service => service.RetrieveAllProjectsAsync(It.IsAny<CancellationToken>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.IsAny<Exception>()), Times.Once);

            this.projectServiceMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
