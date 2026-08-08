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
        public async Task ShouldRetrieveAllPrivateKeysAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/security/keys").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "key-one" }
                    }));

            // when
            IEnumerable<PrivateKey> actualPrivateKeys =
                await this.clientBroker.Client.PrivateKeys.RetrieveAllPrivateKeysAsync();

            // then
            actualPrivateKeys.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/security/keys").UsingGet())
                .Should().ContainSingle();
        }
    }
}
