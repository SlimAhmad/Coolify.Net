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
    public class PrivateKeyClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public PrivateKeyClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldProvisionRetrieveAndRemovePrivateKeyAsync()
        {
            // given
            string privateKeyUuid = Guid.NewGuid().ToString();
            string privateKeyName = "acceptance-key";

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/security/keys").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new { uuid = privateKeyUuid, name = privateKeyName }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = privateKeyUuid, name = privateKeyName }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            var newPrivateKey = new PrivateKey { Name = privateKeyName, PrivateKeyValue = "ssh-rsa AAAA..." };

            // when
            PrivateKey addedPrivateKey =
                await this.apiFixture.Client.PrivateKeys.AddPrivateKeyAsync(newPrivateKey);

            PrivateKey retrievedPrivateKey =
                await this.apiFixture.Client.PrivateKeys.RetrievePrivateKeyByUuidAsync(privateKeyUuid);

            await this.apiFixture.Client.PrivateKeys.RemovePrivateKeyAsync(privateKeyUuid);

            // then
            addedPrivateKey.Uuid.Should().Be(privateKeyUuid);
            retrievedPrivateKey.Name.Should().Be(privateKeyName);

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/security/keys/{privateKeyUuid}").UsingDelete())
                .Should().ContainSingle();
        }

        [Fact]
        public async Task ShouldRetrieveAllPrivateKeysAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/security/keys").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "key-one" }
                    }));

            // when
            IEnumerable<PrivateKey> actualPrivateKeys =
                await this.apiFixture.Client.PrivateKeys.RetrieveAllPrivateKeysAsync();

            // then
            actualPrivateKeys.Should().ContainSingle();
        }
    }
}
