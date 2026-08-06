// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Services.Foundations.Servers;
using Coolify.Resource.Manager.Services.Processings.Servers;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Servers
{
    public partial class ServerProcessingServiceTests
    {
        private readonly Mock<IServerService> serverServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IServerProcessingService serverProcessingService;

        public ServerProcessingServiceTests()
        {
            this.serverServiceMock = new Mock<IServerService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.serverProcessingService = new ServerProcessingService(
                serverService: this.serverServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Server CreateRandomServer() =>
            new Server { Uuid = GetRandomString(), Name = GetRandomString() };
    }
}
