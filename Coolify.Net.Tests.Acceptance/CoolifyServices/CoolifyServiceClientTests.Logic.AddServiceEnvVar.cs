// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.EnvironmentVariables;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.CoolifyServices
{
    public partial class CoolifyServiceClientTests
    {
        [Fact]
        public async Task ShouldAddServiceEnvVarAsync()
        {
            // given
            string serviceUuid = GetRandomString();
            string environmentVariableUuid = GetRandomString();
            var inputEnvironmentVariable = CreateRandomEnvironmentVariable();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = environmentVariableUuid,
                        key = inputEnvironmentVariable.Key,
                        value = inputEnvironmentVariable.Value
                    }));

            // when
            EnvironmentVariable actualEnvironmentVariable =
                await this.clientBroker.Client.CoolifyServices.AddServiceEnvVarAsync(
                    serviceUuid, inputEnvironmentVariable);

            // then
            actualEnvironmentVariable.Uuid.Should().Be(environmentVariableUuid);
            actualEnvironmentVariable.Key.Should().Be(inputEnvironmentVariable.Key);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs").UsingPost())
                .Should().ContainSingle();
        }
    }
}
