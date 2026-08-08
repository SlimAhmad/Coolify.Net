// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Projects
{
    public partial class ProjectClientTests
    {
        [Fact]
        public async Task ShouldRetrieveProjectByUuidAsync()
        {
            // given
            string projectUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = projectUuid,
                        name = "acceptance-project"
                    }));

            // when
            Project actualProject =
                await this.clientBroker.Client.Projects.RetrieveProjectByUuidAsync(projectUuid);

            // then
            actualProject.Uuid.Should().Be(projectUuid);
            actualProject.Name.Should().Be("acceptance-project");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
