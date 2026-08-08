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
        public async Task ShouldRetrieveAllBackupsAsync()
        {
            // given
            string databaseUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/backups").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), frequency = "0 0 * * *", enabled = true }
                    }));

            // when
            IEnumerable<DatabaseBackup> actualBackups =
                await this.clientBroker.Client.Databases.RetrieveAllBackupsAsync(databaseUuid);

            // then
            actualBackups.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/backups").UsingGet())
                .Should().ContainSingle();
        }
    }
}
