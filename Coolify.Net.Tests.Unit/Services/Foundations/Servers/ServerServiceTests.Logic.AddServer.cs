// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Externals.Servers;
using Coolify.Net.Models.Foundations.Servers;
using FluentAssertions;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Servers
{
    public partial class ServerServiceTests
    {
        [Fact]
        public async Task ShouldAddServerAsync()
        {
            // given
            Server randomServer = CreateRandomServer();
            Server inputServer = randomServer;

            ExternalServer inputExternalServer = ConvertToExternalServer(inputServer);
            ExternalServer returnedExternalServer = CreateRandomExternalServer();
            Server expectedServer = ConvertToServer(returnedExternalServer);

            this.coolifyApiBrokerMock
                .Setup(broker => broker.PostServerAsync(
                    It.Is<ExternalServer>(external => IsSameExternalServer(external, inputExternalServer))))
                .ReturnsAsync(returnedExternalServer);

            // when
            Server actualServer =
                await this.serverService.AddServerAsync(inputServer);

            // then
            actualServer.Should().BeEquivalentTo(expectedServer);

            this.coolifyApiBrokerMock.Verify(broker =>
                broker.PostServerAsync(
                    It.Is<ExternalServer>(external => IsSameExternalServer(external, inputExternalServer))),
                Times.Once);

            this.coolifyApiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        private static ExternalServer ConvertToExternalServer(Server server) =>
            new ExternalServer
            {
                Uuid = server.Uuid,
                Name = server.Name,
                Description = server.Description,
                Ip = server.Ip,
                User = server.User,
                Port = server.Port,
                PrivateKeyUuid = server.PrivateKeyUuid,
                ProxyEnabled = server.ProxyEnabled,
                ProxyType = server.ProxyType
            };

        private static bool IsSameExternalServer(ExternalServer actual, ExternalServer expected) =>
            actual.Uuid == expected.Uuid
            && actual.Name == expected.Name
            && actual.Description == expected.Description
            && actual.Ip == expected.Ip
            && actual.User == expected.User
            && actual.Port == expected.Port
            && actual.PrivateKeyUuid == expected.PrivateKeyUuid
            && actual.ProxyEnabled == expected.ProxyEnabled
            && actual.ProxyType == expected.ProxyType;
    }
}
