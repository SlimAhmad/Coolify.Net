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
        public async Task ShouldAddClickHouseDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/clickhouse").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        clickhouse_admin_user = "app_user"
                    }));

            var inputDatabase = new ClickHouseDatabase
            {
                Name = databaseName,
                ClickhouseAdminUser = "app_user"
            };

            // when
            ClickHouseDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddClickHouseDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.ClickhouseAdminUser.Should().Be("app_user");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/clickhouse").UsingPost())
                .Should().ContainSingle();
        }
    }
}
