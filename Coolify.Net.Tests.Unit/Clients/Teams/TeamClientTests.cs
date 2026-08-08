// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Clients.Teams;
using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Models.Processings.Teams.Exceptions;
using Coolify.Net.Services.Processings.Teams;
using Moq;
using Xeptions;

namespace Coolify.Net.Tests.Unit.Clients.Teams
{
    public partial class TeamClientTests
    {
        private readonly Mock<ITeamProcessingService> teamServiceMock;
        private readonly ITeamClient teamClient;

        public TeamClientTests()
        {
            this.teamServiceMock = new Mock<ITeamProcessingService>();

            this.teamClient = new TeamClient(
                teamProcessingService: this.teamServiceMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static int GetRandomId() => new Random().Next(1, 1000);

        private static Team CreateRandomTeam() =>
            new Team { Id = GetRandomId(), Name = GetRandomString() };

        private static TeamMember CreateRandomTeamMember() =>
            new TeamMember { Uuid = GetRandomString(), Name = GetRandomString() };

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
                new TeamProcessingValidationException("test", inner),
                new TeamProcessingDependencyValidationException("test", inner)
            };
        }

        public static TheoryData<Xeption> DependencyAndServiceExceptions()
        {
            Xeption inner = CreateInnerXeption();

            return new TheoryData<Xeption>
            {
                new TeamProcessingDependencyException("test", inner),
                new TeamProcessingServiceException("test", inner)
            };
        }
    }
}
