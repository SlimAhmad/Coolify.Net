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
        public async Task ShouldRetrieveAllServiceEnvVarsAsync()
        {
            // given
            string serviceUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), key = "SOME_KEY", value = "some-value" }
                    }));

            // when
            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.clientBroker.Client.CoolifyServices.RetrieveAllServiceEnvVarsAsync(serviceUuid);

            // then
            actualEnvironmentVariables.Should().ContainSingle(variable => variable.Key == "SOME_KEY");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs").UsingGet())
                .Should().ContainSingle();
        }
    }
}
