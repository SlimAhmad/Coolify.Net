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
    public partial class DatabaseClientTests
    {
        [Fact]
        public async Task ShouldAddPostgreSqlDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
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

            var inputDatabase = new PostgreSqlDatabase
            {
                Name = databaseName,
                PostgresUser = "app_user",
                PostgresDb = "app_db"
            };

            // when
            PostgreSqlDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddPostgreSqlDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.PostgresDb.Should().Be("app_db");

            var postRequest = this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/postgresql").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ClientBroker.ApiToken}");
        }
    }
}
