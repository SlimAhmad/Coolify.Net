// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.PrivateKeys
{
    public partial class PrivateKeyClientTests
    {
        [Fact]
        public async Task ShouldRemovePrivateKeyAsync()
        {
            // given
            string privateKeyUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.PrivateKeys.RemovePrivateKeyAsync(privateKeyUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingDelete())
                .Should().ContainSingle();
        }
    }
}
