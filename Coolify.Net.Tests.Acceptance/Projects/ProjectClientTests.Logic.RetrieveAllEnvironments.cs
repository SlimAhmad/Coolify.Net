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
        public async Task ShouldRetrieveAllEnvironmentsAsync()
        {
            // given
            string projectUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}/environments").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new
                        {
                            uuid = GetRandomString(),
                            name = "production",
                            project_uuid = projectUuid
                        }
                    }));

            // when
            IEnumerable<CoolifyEnvironment> actualEnvironments =
                await this.clientBroker.Client.Projects.RetrieveAllEnvironmentsAsync(projectUuid);

            // then
            actualEnvironments.Should().ContainSingle(environment => environment.Name == "production");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/projects/{projectUuid}/environments").UsingGet())
                .Should().ContainSingle();
        }
    }
}
