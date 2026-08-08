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
        public async Task ShouldAddEnvironmentAsync()
        {
            // given
            string projectUuid = GetRandomString();
            string environmentUuid = GetRandomString();
            string environmentName = "production";

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}/environments").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = environmentUuid,
                        name = environmentName,
                        project_uuid = projectUuid
                    }));

            var inputEnvironment = new CoolifyEnvironment { Name = environmentName };

            // when
            CoolifyEnvironment actualEnvironment =
                await this.clientBroker.Client.Projects.AddEnvironmentAsync(projectUuid, inputEnvironment);

            // then
            actualEnvironment.Uuid.Should().Be(environmentUuid);
            actualEnvironment.ProjectUuid.Should().Be(projectUuid);
            actualEnvironment.Name.Should().Be(environmentName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/projects/{projectUuid}/environments").UsingPost())
                .Should().ContainSingle();
        }
    }
}
