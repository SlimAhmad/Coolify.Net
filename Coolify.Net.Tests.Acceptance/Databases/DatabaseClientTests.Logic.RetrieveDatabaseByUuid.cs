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
        public async Task ShouldRetrieveDatabaseByUuidAsync()
        {
            // given
            string databaseUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = "acceptance-database",
                        database_type = "postgresql"
                    }));

            // when
            Database actualDatabase =
                await this.clientBroker.Client.Databases.RetrieveDatabaseByUuidAsync(databaseUuid);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.DatabaseType.Should().Be("postgresql");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
