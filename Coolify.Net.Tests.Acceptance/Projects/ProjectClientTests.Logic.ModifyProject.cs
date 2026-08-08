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
        public async Task ShouldModifyProjectAsync()
        {
            // given
            string projectUuid = GetRandomString();
            string modifiedName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = projectUuid,
                        name = modifiedName
                    }));

            var inputProject = new Project
            {
                Uuid = projectUuid,
                Name = modifiedName
            };

            // when
            Project actualProject =
                await this.clientBroker.Client.Projects.ModifyProjectAsync(inputProject);

            // then
            actualProject.Uuid.Should().Be(projectUuid);
            actualProject.Name.Should().Be(modifiedName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
