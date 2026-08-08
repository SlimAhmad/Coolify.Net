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
        public async Task ShouldModifyBulkApplicationEnvVarsAsync()
        {
            // given
            string applicationUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs/bulk").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), key = "APP_ENV", value = "production" },
                        new { uuid = GetRandomString(), key = "APP_DEBUG", value = "false" }
                    }));

            var inputEnvVars = new[]
            {
                new EnvironmentVariable { Key = "APP_ENV", Value = "production" },
                new EnvironmentVariable { Key = "APP_DEBUG", Value = "false" }
            };

            // when
            IEnumerable<EnvironmentVariable> actualEnvVars =
                await this.clientBroker.Client.Applications.ModifyBulkApplicationEnvVarsAsync(
                    applicationUuid, inputEnvVars);

            // then
            actualEnvVars.Should().HaveCount(2);

            this.clientBroker.Server
                .FindLogEntries(
                    Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs/bulk").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
