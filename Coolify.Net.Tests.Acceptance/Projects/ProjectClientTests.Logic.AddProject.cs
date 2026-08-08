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
    public partial class ProjectClientTests
    {
        [Fact]
        public async Task ShouldAddProjectAsync()
        {
            // given
            string projectUuid = GetRandomString();
            string projectName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/projects").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = projectUuid,
                        name = projectName
                    }));

            var inputProject = new Project { Name = projectName };

            // when
            Project actualProject =
                await this.clientBroker.Client.Projects.AddProjectAsync(inputProject);

            // then
            actualProject.Uuid.Should().Be(projectUuid);
            actualProject.Name.Should().Be(projectName);

            var postRequest = this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/projects").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ClientBroker.ApiToken}");
        }
    }
}
