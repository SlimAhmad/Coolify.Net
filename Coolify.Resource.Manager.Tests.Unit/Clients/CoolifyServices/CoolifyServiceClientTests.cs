// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Clients.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.CoolifyServices;
using Coolify.Resource.Manager.Models.Foundations.EnvironmentVariables;
using Coolify.Resource.Manager.Models.Processings.CoolifyServices.Exceptions;
using Coolify.Resource.Manager.Services.Processings.CoolifyServices;
using Moq;
using Xeptions;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.CoolifyServices
{
    public partial class CoolifyServiceClientTests
    {
        private readonly Mock<ICoolifyServiceProcessingService> coolifyServiceServiceMock;
        private readonly ICoolifyServiceClient coolifyServiceClient;

        public CoolifyServiceClientTests()
        {
            this.coolifyServiceServiceMock = new Mock<ICoolifyServiceProcessingService>();

            this.coolifyServiceClient = new CoolifyServiceClient(
                coolifyServiceProcessingService: this.coolifyServiceServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static CoolifyService CreateRandomCoolifyService() =>
            new CoolifyService { Uuid = GetRandomString(), Name = GetRandomString() };

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
                new CoolifyServiceProcessingValidationException("test", inner),
                new CoolifyServiceProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new CoolifyServiceProcessingDependencyException("test", inner),
                new CoolifyServiceProcessingServiceException("test", inner)
            };
        }
    }
}
