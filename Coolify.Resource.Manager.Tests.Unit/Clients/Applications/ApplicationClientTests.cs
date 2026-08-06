// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Clients.Applications;
using Coolify.Resource.Manager.Models.Foundations.Applications;
using Coolify.Resource.Manager.Models.Foundations.Applications.Exceptions;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Services.Foundations.Applications;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Applications
{
    public partial class ApplicationClientTests
    {
        private readonly Mock<IApplicationService> applicationServiceMock;
        private readonly IApplicationClient applicationClient;

        public ApplicationClientTests()
        {
            this.applicationServiceMock = new Mock<IApplicationService>();
            this.applicationClient = new ApplicationClient(applicationService: this.applicationServiceMock.Object);
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
                new ApplicationValidationException("test", inner),
                new ApplicationDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ApplicationDependencyException("test", inner),
                new ApplicationServiceException("test", inner)
            };
        }
    }
}
