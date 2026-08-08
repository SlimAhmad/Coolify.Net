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
        public async Task ShouldModifyBackupAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string backupUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create()
                    .WithPath($"/api/v1/databases/{databaseUuid}/backups/{backupUuid}")
                    .UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = backupUuid,
                        frequency = "0 12 * * *",
                        enabled = false
                    }));

            var inputBackup = new DatabaseBackup
            {
                Uuid = backupUuid,
                FrequencyExpression = "0 12 * * *",
                Enabled = false
            };

            // when
            DatabaseBackup actualBackup =
                await this.clientBroker.Client.Databases.ModifyBackupAsync(databaseUuid, inputBackup);

            // then
            actualBackup.Uuid.Should().Be(backupUuid);
            actualBackup.Enabled.Should().BeFalse();

            this.clientBroker.Server
                .FindLogEntries(Request.Create()
                    .WithPath($"/api/v1/databases/{databaseUuid}/backups/{backupUuid}")
                    .UsingPatch())
                .Should().ContainSingle();
        }
    }
}
