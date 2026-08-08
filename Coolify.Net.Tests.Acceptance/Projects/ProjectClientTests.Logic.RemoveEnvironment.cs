// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Projects
{
    public partial class ProjectClientTests
    {
        [Fact]
        public async Task ShouldRemoveEnvironmentAsync()
        {
            // given
            string projectUuid = GetRandomString();
            string environmentNameOrUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create()
                    .WithPath($"/api/v1/projects/{projectUuid}/environments/{environmentNameOrUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.Projects.RemoveEnvironmentAsync(projectUuid, environmentNameOrUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create()
                    .WithPath($"/api/v1/projects/{projectUuid}/environments/{environmentNameOrUuid}").UsingDelete())
                .Should().ContainSingle();
        }
    }
}
