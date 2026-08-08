// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Systems;
using Coolify.Net.Models.Foundations.Systems;
using Coolify.Net.Models.Processings.Systems.Exceptions;
using Coolify.Net.Services.Processings.Systems;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Systems
{
    public partial class SystemClientTests
    {
        private readonly Mock<ISystemProcessingService> systemServiceMock;
        private readonly ISystemClient systemClient;

        public SystemClientTests()
        {
            this.systemServiceMock = new Mock<ISystemProcessingService>();

            this.systemClient = new SystemClient(
                systemProcessingService: this.systemServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static SystemInfo CreateRandomSystemInfo() =>
            new SystemInfo { Version = GetRandomString() };

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
                new SystemProcessingValidationException("test", inner),
                new SystemProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new SystemProcessingDependencyException("test", inner),
                new SystemProcessingServiceException("test", inner)
            };
        }
    }
}
