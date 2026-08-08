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
        public async Task ShouldStopDatabaseAsync()
        {
            // given
            string databaseUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/stop").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.Databases.StopDatabaseAsync(databaseUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/databases/{databaseUuid}/stop").UsingPost())
                .Should().ContainSingle();
        }
    }
}
