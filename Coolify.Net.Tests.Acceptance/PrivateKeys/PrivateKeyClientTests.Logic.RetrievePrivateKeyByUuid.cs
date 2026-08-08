// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.PrivateKeys
{
    public partial class PrivateKeyClientTests
    {
        [Fact]
        public async Task ShouldRetrievePrivateKeyByUuidAsync()
        {
            // given
            string privateKeyUuid = GetRandomString();
            string privateKeyName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = privateKeyUuid, name = privateKeyName }));

            // when
            PrivateKey actualPrivateKey =
                await this.clientBroker.Client.PrivateKeys.RetrievePrivateKeyByUuidAsync(privateKeyUuid);

            // then
            actualPrivateKey.Name.Should().Be(privateKeyName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
