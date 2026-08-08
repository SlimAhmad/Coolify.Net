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
        public async Task ShouldRetrieveAllServicesAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/services").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "service-one" }
                    }));

            // when
            IEnumerable<CoolifyService> actualServices =
                await this.clientBroker.Client.CoolifyServices.RetrieveAllServicesAsync();

            // then
            actualServices.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/services").UsingGet())
                .Should().ContainSingle();
        }
    }
}
