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
        public async Task ShouldRestartServiceAsync()
        {
            // given
            string serviceUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/restart").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.CoolifyServices.RestartServiceAsync(serviceUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/restart").UsingPost())
                .Should().ContainSingle();
        }
    }
}
