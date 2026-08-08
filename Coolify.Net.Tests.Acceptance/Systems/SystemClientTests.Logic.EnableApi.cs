// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Systems
{
    public partial class SystemClientTests
    {
        [Fact]
        public async Task ShouldEnableApiAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/enable").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            bool isEnabled =
                await this.clientBroker.Client.System.EnableApiAsync();

            // then
            isEnabled.Should().BeTrue();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/enable").UsingGet())
                .Should().ContainSingle();
        }
    }
}
