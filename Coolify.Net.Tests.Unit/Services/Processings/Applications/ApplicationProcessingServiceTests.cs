// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Services.Foundations.Applications;
using Coolify.Net.Services.Processings.Applications;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Processings.Applications
{
    public partial class ApplicationProcessingServiceTests
    {
        private readonly Mock<IApplicationService> applicationServiceMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IApplicationProcessingService applicationProcessingService;

        public ApplicationProcessingServiceTests()
        {
            this.applicationServiceMock = new Mock<IApplicationService>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.applicationProcessingService = new ApplicationProcessingService(
                applicationService: this.applicationServiceMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Application CreateRandomApplication() =>
            new Application { Uuid = GetRandomString(), Name = GetRandomString() };

        private static EnvironmentVariable CreateRandomEnvironmentVariable() =>
            new EnvironmentVariable { Uuid = GetRandomString(), Key = GetRandomString() };
    }
}
