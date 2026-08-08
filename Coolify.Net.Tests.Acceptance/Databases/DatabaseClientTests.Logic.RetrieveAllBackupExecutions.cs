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
        public async Task ShouldRetrieveAllBackupExecutionsAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string backupUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create()
                    .WithPath($"/api/v1/databases/{databaseUuid}/backups/{backupUuid}/executions")
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), status = "success" }
                    }));

            // when
            IEnumerable<BackupExecution> actualExecutions =
                await this.clientBroker.Client.Databases.RetrieveAllBackupExecutionsAsync(databaseUuid, backupUuid);

            // then
            actualExecutions.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create()
                    .WithPath($"/api/v1/databases/{databaseUuid}/backups/{backupUuid}/executions")
                    .UsingGet())
                .Should().ContainSingle();
        }
    }
}
