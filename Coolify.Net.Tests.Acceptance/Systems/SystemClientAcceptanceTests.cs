// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Systems
{
    public class SystemClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public SystemClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldRetrieveVersionAndCheckHealthAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/version").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { version = "4.0.0" }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/healthcheck").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            SystemInfo systemInfo =
                await this.apiFixture.Client.System.RetrieveVersionAsync();

            bool isHealthy =
                await this.apiFixture.Client.System.CheckHealthAsync();

            // then
            systemInfo.Version.Should().Be("4.0.0");
            isHealthy.Should().BeTrue();
        }
    }
}
