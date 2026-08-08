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
        public async Task ShouldAddBackupAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string backupUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/backups").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = backupUuid,
                        frequency = "0 0 * * *",
                        enabled = true
                    }));

            var inputBackup = new DatabaseBackup
            {
                FrequencyExpression = "0 0 * * *",
                Enabled = true
            };

            // when
            DatabaseBackup actualBackup =
                await this.clientBroker.Client.Databases.AddBackupAsync(databaseUuid, inputBackup);

            // then
            actualBackup.Uuid.Should().Be(backupUuid);
            actualBackup.FrequencyExpression.Should().Be("0 0 * * *");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/backups").UsingPost())
                .Should().ContainSingle();
        }
    }
}
