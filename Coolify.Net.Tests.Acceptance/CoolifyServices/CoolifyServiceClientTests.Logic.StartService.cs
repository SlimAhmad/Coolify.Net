// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.CoolifyServices
{
    public partial class CoolifyServiceClientTests
    {
        [Fact]
        public async Task ShouldStartServiceAsync()
        {
            // given
            string serviceUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/start").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.CoolifyServices.StartServiceAsync(serviceUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/start").UsingPost())
                .Should().ContainSingle();
        }
    }
}
