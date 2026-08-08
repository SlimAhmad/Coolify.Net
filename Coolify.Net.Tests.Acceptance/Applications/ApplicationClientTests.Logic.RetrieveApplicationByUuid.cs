// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldRetrieveApplicationByUuidAsync()
        {
            // given
            string applicationUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = applicationUuid,
                        name = "acceptance-application",
                        git_repository = "https://github.com/coollabsio/coolify-examples",
                        git_branch = "main"
                    }));

            // when
            Application actualApplication =
                await this.clientBroker.Client.Applications.RetrieveApplicationByUuidAsync(applicationUuid);

            // then
            actualApplication.Uuid.Should().Be(applicationUuid);
            actualApplication.GitBranch.Should().Be("main");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
