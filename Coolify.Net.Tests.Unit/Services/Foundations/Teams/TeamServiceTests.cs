// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Linq.Expressions;
using System.Net;
using Coolify.Net.Brokers.CoolifyApis;
using Coolify.Net.Brokers.Loggings;
using Coolify.Net.Models.Externals.Teams;
using Coolify.Net.Models.Foundations.Teams;
using Coolify.Net.Models.Foundations.Teams.Exceptions;
using Coolify.Net.Services.Foundations.Teams;
using Moq;
using Xeptions;

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

        private static HttpRequestException CreateHttpRequestException(HttpStatusCode statusCode) =>
            new HttpRequestException(
                message: "HTTP error occurred.",
                inner: null,
                statusCode: statusCode);

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static TeamDependencyValidationException CreateInvalidTeamDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var invalidTeamException = new InvalidTeamException(
                message: "Invalid team.",
                innerException: httpRequestException);

            return new TeamDependencyValidationException(
                message: "Team dependency validation error occurred, fix the errors and try again.",
                innerException: invalidTeamException);
        }

        private static TeamDependencyValidationException CreateAlreadyExistsTeamDependencyValidationException(
            HttpRequestException httpRequestException)
        {
            var alreadyExistsTeamException = new AlreadyExistsTeamException(
                message: "Team already exists.",
                innerException: httpRequestException);

            return new TeamDependencyValidationException(
                message: "Team dependency validation error occurred, fix the errors and try again.",
                innerException: alreadyExistsTeamException);
        }

        private static TeamDependencyException CreateFailedTeamDependencyException(
            HttpRequestException httpRequestException)
        {
            var failedTeamDependencyException = new FailedTeamDependencyException(
                message: "Failed team dependency error occurred.",
                innerException: httpRequestException);

            return new TeamDependencyException(
                message: "Team dependency error occurred, contact support.",
                innerException: failedTeamDependencyException);
        }

        private static TeamServiceException CreateFailedTeamServiceException(Exception exception)
        {
            var failedTeamServiceException = new FailedTeamServiceException(
                message: "Failed team service error occurred.",
                innerException: exception);

            return new TeamServiceException(
                message: "Team service error occurred, contact support.",
                innerException: failedTeamServiceException);
        }
    }
}
