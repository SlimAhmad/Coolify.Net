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
        public async Task ShouldRetrieveAllProjectsAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/projects").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "project-one" }
                    }));

            // when
            IEnumerable<Project> actualProjects =
                await this.clientBroker.Client.Projects.RetrieveAllProjectsAsync();

            // then
            actualProjects.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/projects").UsingGet())
                .Should().ContainSingle();
        }
    }
}
