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
        public async Task ShouldModifyCoolifyServiceAsync()
        {
            // given
            string serviceUuid = GetRandomString();
            string modifiedName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = serviceUuid, name = modifiedName }));

            var inputService = new CoolifyService
            {
                Uuid = serviceUuid,
                Name = modifiedName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString()
            };

            // when
            CoolifyService actualService =
                await this.clientBroker.Client.CoolifyServices.ModifyCoolifyServiceAsync(inputService);

            // then
            actualService.Name.Should().Be(modifiedName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
