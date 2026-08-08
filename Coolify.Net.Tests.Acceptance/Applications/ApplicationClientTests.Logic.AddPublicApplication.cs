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
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldAddPublicApplicationAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string applicationName = GetRandomString();

            this.clientBroker.Server
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

            var inputApplication = new Application
            {
                Name = applicationName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                GitRepository = "https://github.com/coollabsio/coolify-examples",
                GitBranch = "main"
            };

            // when
            Application actualApplication =
                await this.clientBroker.Client.Applications.AddPublicApplicationAsync(inputApplication);

            // then
            actualApplication.Uuid.Should().Be(applicationUuid);
            actualApplication.Name.Should().Be(applicationName);

            var postRequest = this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/applications/public").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ClientBroker.ApiToken}");
        }
    }
}
