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
        public async Task ShouldRemoveProjectAsync()
        {
            // given
            string projectUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.Projects.RemoveProjectAsync(projectUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/projects/{projectUuid}").UsingDelete())
                .Should().ContainSingle();
        }
    }
}
