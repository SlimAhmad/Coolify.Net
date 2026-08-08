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
        public async Task ShouldAddMySqlDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/mysql").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        mysql_user = "app_user",
                        mysql_database = "app_db"
                    }));

            var inputDatabase = new MySqlDatabase
            {
                Name = databaseName,
                MysqlUser = "app_user",
                MysqlDatabase = "app_db"
            };

            // when
            MySqlDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddMySqlDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.MysqlDatabase.Should().Be("app_db");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/mysql").UsingPost())
                .Should().ContainSingle();
        }
    }
}
