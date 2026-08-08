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
        public async Task ShouldAddMariaDbDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/mariadb").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        mariadb_user = "app_user",
                        mariadb_database = "app_db"
                    }));

            var inputDatabase = new MariaDbDatabase
            {
                Name = databaseName,
                MariadbUser = "app_user",
                MariadbDatabase = "app_db"
            };

            // when
            MariaDbDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddMariaDbDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.MariadbDatabase.Should().Be("app_db");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/mariadb").UsingPost())
                .Should().ContainSingle();
        }
    }
}
