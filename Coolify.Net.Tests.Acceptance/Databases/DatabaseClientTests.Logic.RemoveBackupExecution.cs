// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Databases
{
    public partial class DatabaseClientTests
    {
        [Fact]
        public async Task ShouldRemoveBackupExecutionAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string backupUuid = GetRandomString();
            string executionUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create()
                    .WithPath($"/api/v1/databases/{databaseUuid}/backups/{backupUuid}/executions/{executionUuid}")
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.Databases.RemoveBackupExecutionAsync(
                databaseUuid, backupUuid, executionUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create()
                    .WithPath($"/api/v1/databases/{databaseUuid}/backups/{backupUuid}/executions/{executionUuid}")
                    .UsingDelete())
                .Should().ContainSingle();
        }
    }
}
