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
        public async Task ShouldModifyPrivateKeyAsync()
        {
            // given
            string privateKeyUuid = GetRandomString();
            string modifiedName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = privateKeyUuid, name = modifiedName }));

            var inputPrivateKey = new PrivateKey
            {
                Uuid = privateKeyUuid,
                Name = modifiedName,
                PrivateKeyValue = "ssh-rsa AAAA..."
            };

            // when
            PrivateKey actualPrivateKey =
                await this.clientBroker.Client.PrivateKeys.ModifyPrivateKeyAsync(inputPrivateKey);

            // then
            actualPrivateKey.Name.Should().Be(modifiedName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
