// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Databases;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Databases
{
    public class DatabaseClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public DatabaseClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldProvisionStartAndRemovePostgresDatabaseAsync()
        {
            // given
            string databaseUuid = Guid.NewGuid().ToString();
            string databaseName = "acceptance-postgres";

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/databases/postgresql").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        postgres_user = "app_user",
                        postgres_db = "app_db"
                    }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/start").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            var newDatabase = new PostgreSqlDatabase
            {
                Name = databaseName,
                PostgresUser = "app_user",
                PostgresDb = "app_db"
            };

            // when
            PostgreSqlDatabase addedDatabase =
                await this.apiFixture.Client.Databases.AddPostgreSqlDatabaseAsync(newDatabase);

            await this.apiFixture.Client.Databases.StartDatabaseAsync(databaseUuid);
            await this.apiFixture.Client.Databases.RemoveDatabaseAsync(databaseUuid);

            // then
            addedDatabase.Uuid.Should().Be(databaseUuid);
            addedDatabase.PostgresDb.Should().Be("app_db");

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}").UsingDelete())
                .Should().ContainSingle();
        }

        [Fact]
        public async Task ShouldRetrieveAllDatabasesAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/databases").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "database-one" }
                    }));

            // when
            IEnumerable<Database> actualDatabases =
                await this.apiFixture.Client.Databases.RetrieveAllDatabasesAsync();

            // then
            actualDatabases.Should().ContainSingle();
        }
    }
}
