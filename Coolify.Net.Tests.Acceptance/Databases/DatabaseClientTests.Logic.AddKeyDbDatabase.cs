// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Databases;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Databases
{
    public partial class DatabaseClientTests
    {
        [Fact]
        public async Task ShouldAddKeyDbDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/keydb").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        keydb_password = "secret"
                    }));

            var inputDatabase = new KeyDbDatabase
            {
                Name = databaseName,
                KeydbPassword = "secret"
            };

            // when
            KeyDbDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddKeyDbDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.KeydbPassword.Should().Be("secret");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/keydb").UsingPost())
                .Should().ContainSingle();
        }
    }
}
