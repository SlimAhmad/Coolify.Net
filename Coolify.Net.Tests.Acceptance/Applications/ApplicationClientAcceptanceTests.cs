// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    public class ApplicationClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public ApplicationClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldProvisionDeployAndRemoveApplicationAsync()
        {
            // given
            string applicationUuid = Guid.NewGuid().ToString();
            string applicationName = "acceptance-website";

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/applications/public").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = applicationUuid,
                        name = applicationName,
                        git_repository = "https://github.com/coollabsio/coolify-examples",
                        git_branch = "main"
                    }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/start").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            var newApplication = new Application
            {
                Name = applicationName,
                ServerUuid = Guid.NewGuid().ToString(),
                ProjectUuid = Guid.NewGuid().ToString(),
                GitRepository = "https://github.com/coollabsio/coolify-examples",
                GitBranch = "main"
            };

            // when
            Application addedApplication =
                await this.apiFixture.Client.Applications.AddPublicApplicationAsync(newApplication);

            await this.apiFixture.Client.Applications.StartApplicationAsync(applicationUuid);
            await this.apiFixture.Client.Applications.RemoveApplicationAsync(applicationUuid);

            // then
            addedApplication.Uuid.Should().Be(applicationUuid);
            addedApplication.GitRepository.Should().Be(newApplication.GitRepository);

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/start").UsingPost())
                .Should().ContainSingle();
        }

        [Fact]
        public async Task ShouldRetrieveAllApplicationsAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/applications").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "application-one" }
                    }));

            // when
            IEnumerable<Application> actualApplications =
                await this.apiFixture.Client.Applications.RetrieveAllApplicationsAsync();

            // then
            actualApplications.Should().ContainSingle();
        }
    }
}
