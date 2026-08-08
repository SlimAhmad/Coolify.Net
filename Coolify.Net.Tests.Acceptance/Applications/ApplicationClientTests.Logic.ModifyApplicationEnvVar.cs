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
        public async Task ShouldModifyApplicationEnvVarAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string envVarUuid = GetRandomString();
            string modifiedValue = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = envVarUuid,
                        key = "APP_ENV",
                        value = modifiedValue
                    }));

            var inputEnvVar = new EnvironmentVariable
            {
                Uuid = envVarUuid,
                Key = "APP_ENV",
                Value = modifiedValue
            };

            // when
            EnvironmentVariable actualEnvVar =
                await this.clientBroker.Client.Applications.ModifyApplicationEnvVarAsync(applicationUuid, inputEnvVar);

            // then
            actualEnvVar.Uuid.Should().Be(envVarUuid);
            actualEnvVar.Value.Should().Be(modifiedValue);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
