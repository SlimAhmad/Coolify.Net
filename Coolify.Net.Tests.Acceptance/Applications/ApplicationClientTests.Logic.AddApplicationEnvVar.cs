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
        public async Task ShouldAddApplicationEnvVarAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string envVarUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = envVarUuid,
                        key = "APP_ENV",
                        value = "production"
                    }));

            var inputEnvVar = new EnvironmentVariable
            {
                Key = "APP_ENV",
                Value = "production"
            };

            // when
            EnvironmentVariable actualEnvVar =
                await this.clientBroker.Client.Applications.AddApplicationEnvVarAsync(applicationUuid, inputEnvVar);

            // then
            actualEnvVar.Uuid.Should().Be(envVarUuid);
            actualEnvVar.Key.Should().Be("APP_ENV");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}/envs").UsingPost())
                .Should().ContainSingle();
        }
    }
}
