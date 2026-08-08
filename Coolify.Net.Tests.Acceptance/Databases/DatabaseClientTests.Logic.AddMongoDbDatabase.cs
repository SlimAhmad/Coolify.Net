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
        public async Task ShouldAddMongoDbDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/mongodb").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        mongo_initdb_root_username = "app_user",
                        mongo_initdb_database = "app_db"
                    }));

            var inputDatabase = new MongoDbDatabase
            {
                Name = databaseName,
                MongoInitdbRootUsername = "app_user",
                MongoInitdbDatabase = "app_db"
            };

            // when
            MongoDbDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddMongoDbDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.MongoInitdbDatabase.Should().Be("app_db");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/mongodb").UsingPost())
                .Should().ContainSingle();
        }
    }
}
