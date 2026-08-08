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
        public async Task ShouldAddRedisDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();
            string databaseName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/databases/redis").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = databaseUuid,
                        name = databaseName,
                        redis_password = "secret"
                    }));

            var inputDatabase = new RedisDatabase
            {
                Name = databaseName,
                RedisPassword = "secret"
            };

            // when
            RedisDatabase actualDatabase =
                await this.clientBroker.Client.Databases.AddRedisDatabaseAsync(inputDatabase);

            // then
            actualDatabase.Uuid.Should().Be(databaseUuid);
            actualDatabase.RedisPassword.Should().Be("secret");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/databases/redis").UsingPost())
                .Should().ContainSingle();
        }
    }
}
