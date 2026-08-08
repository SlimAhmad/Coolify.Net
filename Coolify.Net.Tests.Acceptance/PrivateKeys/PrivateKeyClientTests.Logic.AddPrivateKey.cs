// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.PrivateKeys;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.PrivateKeys
{
    public partial class PrivateKeyClientTests
    {
        [Fact]
        public async Task ShouldAddPrivateKeyAsync()
        {
            // given
            string privateKeyUuid = GetRandomString();
            string privateKeyName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/security/keys").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new { uuid = privateKeyUuid, name = privateKeyName }));

            var inputPrivateKey = new PrivateKey { Name = privateKeyName, PrivateKeyValue = "ssh-rsa AAAA..." };

            // when
            PrivateKey actualPrivateKey =
                await this.clientBroker.Client.PrivateKeys.AddPrivateKeyAsync(inputPrivateKey);

            // then
            actualPrivateKey.Uuid.Should().Be(privateKeyUuid);
            actualPrivateKey.Name.Should().Be(privateKeyName);

            var postRequest = this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/security/keys").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ClientBroker.ApiToken}");
        }
    }
}
