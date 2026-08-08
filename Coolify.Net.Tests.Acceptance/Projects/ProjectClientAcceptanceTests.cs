// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Projects;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Projects
{
    public class ProjectClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public ProjectClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldProvisionProjectWithEnvironmentThenDeprovisionAsync()
        {
            // given
            string projectUuid = Guid.NewGuid().ToString();
            string projectName = "acceptance-project";
            string environmentUuid = Guid.NewGuid().ToString();
            string environmentName = "production";

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/projects").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new { uuid = projectUuid, name = projectName }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}/environments").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = environmentUuid,
                        name = environmentName,
                        project_uuid = projectUuid
                    }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            var newProject = new Project { Name = projectName };

            // when
            Project addedProject =
                await this.apiFixture.Client.Projects.AddProjectAsync(newProject);

            CoolifyEnvironment addedEnvironment =
                await this.apiFixture.Client.Projects.AddEnvironmentAsync(
                    projectUuid,
                    new CoolifyEnvironment { Name = environmentName });

            await this.apiFixture.Client.Projects.RemoveProjectAsync(projectUuid);

            // then
            addedProject.Uuid.Should().Be(projectUuid);
            addedEnvironment.ProjectUuid.Should().Be(projectUuid);
            addedEnvironment.Name.Should().Be(environmentName);

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingDelete())
                .Should().ContainSingle();
        }

        [Fact]
        public async Task ShouldRetrieveAllProjectsAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/projects").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "project-one" }
                    }));

            // when
            IEnumerable<Project> actualProjects =
                await this.apiFixture.Client.Projects.RetrieveAllProjectsAsync();

            // then
            actualProjects.Should().ContainSingle();
        }
    }
}
