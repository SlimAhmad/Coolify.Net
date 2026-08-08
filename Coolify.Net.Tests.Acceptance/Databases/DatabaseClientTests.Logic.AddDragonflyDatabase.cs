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
        public async Task ShouldAddDragonflyDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/dragonfly").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        dragonfly_password = "secret"
                    }));

            var inputDatabase = new DragonflyDatabase
            {
                Name = databaseName,
                DragonflyPassword = "secret"
            };

            // when
            DragonflyDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddDragonflyDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.DragonflyPassword.Should().Be("secret");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/dragonfly").UsingPost())
                .Should().ContainSingle();
        }
    }
}
