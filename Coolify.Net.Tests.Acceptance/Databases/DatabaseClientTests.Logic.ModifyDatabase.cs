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
        public async Task ShouldModifyDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string modifiedName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = modifiedName
                    }));

            var inputDatabase = new Database
            {
                Uuid = databaseUuid,
                Name = modifiedName
            };

            // when
            Database actualDatabase =
                await this.clientBroker.Client.Databases.ModifyDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.Name.Should().Be(modifiedName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
