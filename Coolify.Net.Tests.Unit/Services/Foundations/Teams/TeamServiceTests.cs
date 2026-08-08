// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Teams;
using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Services.Foundations.Teams;
using Moq;

namespace Coolify.Net.Tests.Unit.Services.Foundations.Teams
{
    public partial class TeamServiceTests
    {
        private readonly Mock<ICoolifyApiBroker> coolifyApiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ITeamService teamService;

        public TeamServiceTests()
        {
            this.coolifyApiBrokerMock = new Mock<ICoolifyApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.teamService = new TeamService(
                coolifyApiBroker: this.coolifyApiBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static int GetRandomId() => new Random().Next(1, 1000);

        private static ExternalTeam CreateRandomExternalTeam() =>
            new ExternalTeam
            {
                Id = GetRandomId(),
                Name = GetRandomString(),
                Description = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static Team ConvertToTeam(ExternalTeam externalTeam) =>
            new Team
            {
                Id = externalTeam.Id,
                Name = externalTeam.Name,
                Description = externalTeam.Description,
                CreatedAt = externalTeam.CreatedAt,
                UpdatedAt = externalTeam.UpdatedAt
            };

        private static ExternalTeamMember CreateRandomExternalTeamMember() =>
            new ExternalTeamMember
            {
                Uuid = GetRandomString(),
                Name = GetRandomString(),
                Email = GetRandomString(),
                Role = GetRandomString(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

        private static TeamMember ConvertToTeamMember(ExternalTeamMember externalTeamMember) =>
            new TeamMember
            {
                Uuid = externalTeamMember.Uuid,
                Name = externalTeamMember.Name,
                Email = externalTeamMember.Email,
                Role = externalTeamMember.Role,
                CreatedAt = externalTeamMember.CreatedAt,
                UpdatedAt = externalTeamMember.UpdatedAt
            };

        public static TheoryData<HttpStatusCode> DependencyValidationHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.BadRequest,
                HttpStatusCode.Conflict
            };

        public static TheoryData<HttpStatusCode> CriticalDependencyHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.Unauthorized,
                HttpStatusCode.Forbidden,
                HttpStatusCode.NotFound
            };

        public static TheoryData<HttpStatusCode> DependencyHttpStatusCodes() =>
            new TheoryData<HttpStatusCode>
            {
                HttpStatusCode.TooManyRequests,
                HttpStatusCode.ServiceUnavailable,
                HttpStatusCode.InternalServerError
            };

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);
    }
}
