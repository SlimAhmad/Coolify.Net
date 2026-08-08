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
        public async Task ShouldAddPrivateGithubAppApplicationAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string applicationName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/applications/private-github-app").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = applicationUuid,
                        name = applicationName,
                        git_repository = "org/private-repo",
                        git_branch = "main"
                    }));

            var inputApplication = new Application
            {
                Name = applicationName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                GitRepository = "org/private-repo",
                GitBranch = "main"
            };

            // when
            Application actualApplication =
                await this.clientBroker.Client.Applications.AddPrivateGithubAppApplicationAsync(inputApplication);

            // then
            actualApplication.Uuid.Should().Be(applicationUuid);
            actualApplication.GitRepository.Should().Be("org/private-repo");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/applications/private-github-app").UsingPost())
                .Should().ContainSingle();
        }
    }
}
