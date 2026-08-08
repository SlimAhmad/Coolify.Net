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
        public async Task ShouldModifyServiceEnvVarAsync()
        {
            // given
            string serviceUuid = GetRandomString();
            var inputEnvironmentVariable = CreateRandomEnvironmentVariable();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = inputEnvironmentVariable.Uuid,
                        key = inputEnvironmentVariable.Key,
                        value = inputEnvironmentVariable.Value
                    }));

            // when
            EnvironmentVariable actualEnvironmentVariable =
                await this.clientBroker.Client.CoolifyServices.ModifyServiceEnvVarAsync(
                    serviceUuid, inputEnvironmentVariable);

            // then
            actualEnvironmentVariable.Key.Should().Be(inputEnvironmentVariable.Key);
            actualEnvironmentVariable.Value.Should().Be(inputEnvironmentVariable.Value);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
