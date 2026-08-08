// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Services.Foundations.CoolifyServices;
using Coolify.Net.Services.Processings.CoolifyServices;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.CoolifyServices
{
    public partial class CoolifyServiceProcessingServiceTests
    {
        private readonly Mock<ICoolifyServiceService> coolifyServiceServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICoolifyServiceProcessingService coolifyServiceProcessingService;

        public CoolifyServiceProcessingServiceTests()
        {
            this.coolifyServiceServiceMock = new Mock<ICoolifyServiceService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.coolifyServiceProcessingService = new CoolifyServiceProcessingService(
                coolifyServiceService: this.coolifyServiceServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static CoolifyService CreateRandomCoolifyService() =>
            new CoolifyService { Uuid = GetRandomString(), Name = GetRandomString() };

        private static EnvironmentVariable CreateRandomEnvironmentVariable() =>
            new EnvironmentVariable { Uuid = GetRandomString(), Key = GetRandomString() };
    }
}
