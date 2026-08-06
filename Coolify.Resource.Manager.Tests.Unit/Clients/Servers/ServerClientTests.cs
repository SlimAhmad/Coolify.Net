// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Resource.Manager.Clients.Servers;
using Coolify.Resource.Manager.Models.Foundations.Servers;
using Coolify.Resource.Manager.Models.Processings.Servers.Exceptions;
using Coolify.Resource.Manager.Services.Processings.Servers;
using Moq;
using Xeptions;
using Xunit;

namespace Coolify.Resource.Manager.Tests.Unit.Clients.Servers
{
    public partial class ServerClientTests
    {
        private readonly Mock<IServerProcessingService> serverServiceMock;
        private readonly IServerClient serverClient;

        public ServerClientTests()
        {
            this.serverServiceMock = new Mock<IServerProcessingService>();
            this.serverClient = new ServerClient(serverProcessingService: this.serverServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static Server CreateRandomServer() =>
            new Server { Uuid = GetRandomString(), Name = GetRandomString() };

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
                new ServerProcessingValidationException("test", inner),
                new ServerProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new ServerProcessingDependencyException("test", inner),
                new ServerProcessingServiceException("test", inner)
            };
        }
    }
}
