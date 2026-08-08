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
        public async Task ShouldDisableApiAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/disable").UsingGet())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            bool isDisabled =
                await this.clientBroker.Client.System.DisableApiAsync();

            // then
            isDisabled.Should().BeTrue();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/disable").UsingGet())
                .Should().ContainSingle();
        }
    }
}
