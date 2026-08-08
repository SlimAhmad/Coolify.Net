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
        public async Task ShouldRetrieveEnvironmentAsync()
        {
            // given
            string projectUuid = GetRandomString();
            string environmentNameOrUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create()
                    .WithPath($"/api/v1/projects/{projectUuid}/{environmentNameOrUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = environmentNameOrUuid,
                        name = "production",
                        project_uuid = projectUuid
                    }));

            // when
            CoolifyEnvironment actualEnvironment =
                await this.clientBroker.Client.Projects.RetrieveEnvironmentAsync(
                    projectUuid, environmentNameOrUuid);

            // then
            actualEnvironment.ProjectUuid.Should().Be(projectUuid);
            actualEnvironment.Name.Should().Be("production");

            this.clientBroker.Server
                .FindLogEntries(Request.Create()
                    .WithPath($"/api/v1/projects/{projectUuid}/{environmentNameOrUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
