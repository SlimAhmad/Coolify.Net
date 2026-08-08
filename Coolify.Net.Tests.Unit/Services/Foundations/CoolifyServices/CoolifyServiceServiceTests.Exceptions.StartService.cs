// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Models.Foundations.CoolifyServices.Exceptions;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.CoolifyServices
{
    public partial class CoolifyServiceServiceTests
    {
        [Theory]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.Forbidden)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.Conflict)]
        [InlineData(HttpStatusCode.TooManyRequests)]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        public async Task ShouldThrowDependencyExceptionOnStartServiceIfHttpErrorOccursAndLogItAsync(
            HttpStatusCode statusCode)
        {
            // given
            string someServiceUuid = GetRandomString();
            HttpRequestException httpRequestException = CreateHttpRequestException(statusCode);

            CoolifyServiceDependencyException expectedException =
                CreateFailedCoolifyServiceDependencyException(httpRequestException);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceStartAsync(someServiceUuid))
                .ThrowsAsync(httpRequestException);

            // when
            ValueTask startServiceTask =
                this.coolifyServiceService.StartServiceAsync(someServiceUuid);

            CoolifyServiceDependencyException actualException =
                await Assert.ThrowsAsync<CoolifyServiceDependencyException>(startServiceTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceStartAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnStartServiceIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string someServiceUuid = GetRandomString();
            var exception = new Exception("Unexpected error.");

            CoolifyServiceServiceException expectedException =
                CreateFailedCoolifyServiceServiceException(exception);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServiceStartAsync(someServiceUuid))
                .ThrowsAsync(exception);

            // when
            ValueTask startServiceTask =
                this.coolifyServiceService.StartServiceAsync(someServiceUuid);

            CoolifyServiceServiceException actualException =
                await Assert.ThrowsAsync<CoolifyServiceServiceException>(startServiceTask.AsTask);

            // then
            actualException.Should().BeEquivalentTo(expectedException);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServiceStartAsync(someServiceUuid), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogErrorAsync(It.Is(SameExceptionAs(expectedException))), Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
