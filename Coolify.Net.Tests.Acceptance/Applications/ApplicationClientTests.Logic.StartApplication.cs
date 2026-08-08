// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldStartApplicationAsync()
        {
            // given
            string applicationUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/start").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.Applications.StartApplicationAsync(applicationUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/start").UsingPost())
                .Should().ContainSingle();
        }
    }
}
