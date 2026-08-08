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
        public async Task ShouldRetrieveAllDatabasesAsync()
        {
            // given
            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), name = "database-one" },
                        new { uuid = GetRandomString(), name = "database-two" }
                    }));

            // when
            IEnumerable<Database> actualDatabases =
                await this.clientBroker.Client.Databases.RetrieveAllDatabasesAsync();

            // then
            actualDatabases.Should().HaveCount(2);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases").UsingGet())
                .Should().ContainSingle();
        }
    }
}
