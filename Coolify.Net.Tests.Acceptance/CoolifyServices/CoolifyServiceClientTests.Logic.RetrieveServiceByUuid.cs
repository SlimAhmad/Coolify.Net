// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.CoolifyServices
{
    public partial class CoolifyServiceClientTests
    {
        [Fact]
        public async Task ShouldRetrieveServiceByUuidAsync()
        {
            // given
            string serviceUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = serviceUuid, name = "acceptance-service" }));

            // when
            CoolifyService actualService =
                await this.clientBroker.Client.CoolifyServices.RetrieveServiceByUuidAsync(serviceUuid);

            // then
            actualService.Uuid.Should().Be(serviceUuid);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
