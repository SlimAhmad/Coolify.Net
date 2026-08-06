// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Clients.Applications;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Processings.Applications.Exceptions;
using Coolify.Resource.Manager.Services.Processings.Applications;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Applications
{
    public partial class ApplicationClientTests
    {
        private readonly Mock<IApplicationProcessingService> applicationServiceMock;
        private readonly IApplicationClient applicationClient;

        public ApplicationClientTests()
        {
            this.applicationServiceMock = new Mock<IApplicationProcessingService>();

            this.applicationClient = new ApplicationClient(
                applicationProcessingService: this.applicationServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Application CreateRandomApplication() =>
            new Application { Uuid = GetRandomString(), Name = GetRandomString() };

        private static EnvironmentVariable CreateRandomEnvironmentVariable() =>
            new EnvironmentVariable { Uuid = GetRandomString(), Key = GetRandomString() };

        private static Xeption CreateInnerXeption()
        {
            var inner = new Xeption(GetRandomString());
            inner.AddData(GetRandomString(), GetRandomString());

            return inner;
        }

        public static TheoryData<Xeption> ValidationExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ApplicationProcessingValidationException("test", inner),
                new ApplicationProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ApplicationProcessingDependencyException("test", inner),
                new ApplicationProcessingServiceException("test", inner)
            };
        }
    }
}
