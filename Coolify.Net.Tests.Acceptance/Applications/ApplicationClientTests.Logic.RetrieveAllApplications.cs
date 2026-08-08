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
        public async Task ShouldRetrieveAllApplicationsAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/applications").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "application-one" },
                        new { uuid = GetRandomString(), name = "application-two" }
                    }));

            // when
            IEnumerable<Application> actualApplications =
                await this.clientBroker.Client.Applications.RetrieveAllApplicationsAsync();

            // then
            actualApplications.Should().HaveCount(2);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/applications").UsingGet())
                .Should().ContainSingle();
        }
    }
}
