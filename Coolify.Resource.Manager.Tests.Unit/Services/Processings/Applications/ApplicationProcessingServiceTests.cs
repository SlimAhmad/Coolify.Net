// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Brokers.Loggings;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Services.Foundations.Applications;
using Coolify.Resource.Manager.Services.Processings.Applications;
using Moq;

namespace Coolify.Resource.Manager.Tests.Unit.Services.Processings.Applications
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
