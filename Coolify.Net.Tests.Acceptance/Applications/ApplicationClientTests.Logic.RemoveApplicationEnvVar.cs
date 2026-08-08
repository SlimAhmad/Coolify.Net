// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldRemoveApplicationEnvVarAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string envVarUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create()
                    .WithPath($"/api/v1/applications/{applicationUuid}/envs/{envVarUuid}")
                    .UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            // when
            await this.clientBroker.Client.Applications.RemoveApplicationEnvVarAsync(applicationUuid, envVarUuid);

            // then
            this.clientBroker.Server
                .FindLogEntries(Request.Create()
                    .WithPath($"/api/v1/applications/{applicationUuid}/envs/{envVarUuid}")
                    .UsingDelete())
                .Should().ContainSingle();
        }
    }
}
