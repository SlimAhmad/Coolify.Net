// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldRetrieveAllApplicationEnvVarsAsync()
        {
            // given
            string applicationUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), key = "APP_ENV", value = "production" }
                    }));

            // when
            IEnumerable<EnvironmentVariable> actualEnvVars =
                await this.clientBroker.Client.Applications.RetrieveAllApplicationEnvVarsAsync(applicationUuid);

            // then
            actualEnvVars.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs").UsingGet())
                .Should().ContainSingle();
        }
    }
}
