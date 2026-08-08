// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Systems;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Systems
{
    public partial class SystemClientTests
    {
        [Fact]
        public async Task ShouldRetrieveVersionAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/version").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { version = "4.0.0" }));

            // when
            SystemInfo actualSystemInfo =
                await this.clientBroker.Client.System.RetrieveVersionAsync();

            // then
            actualSystemInfo.Version.Should().Be("4.0.0");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/version").UsingGet())
                .Should().ContainSingle();
        }
    }
}
