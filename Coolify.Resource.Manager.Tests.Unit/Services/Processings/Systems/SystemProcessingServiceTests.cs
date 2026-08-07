// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Systems;
using Coolify.Resource.Manager.Services.Foundations.Systems;
using Coolify.Resource.Manager.Services.Processings.Systems;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Systems
{
    public partial class SystemProcessingServiceTests
    {
        private readonly Mock<ISystemService> systemServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ISystemProcessingService systemProcessingService;

        public SystemProcessingServiceTests()
        {
            this.systemServiceMock = new Mock<ISystemService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.systemProcessingService = new SystemProcessingService(
                systemService: this.systemServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static SystemInfo CreateRandomSystemInfo() =>
            new SystemInfo { Version = GetRandomString() };
    }
}
