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
        public async Task ShouldModifyBulkServiceEnvVarsAsync()
        {
            // given
            string serviceUuid = GetRandomString();

            var inputEnvironmentVariables = new[]
            {
                CreateRandomEnvironmentVariable(),
                CreateRandomEnvironmentVariable()
            };

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs/bulk").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(inputEnvironmentVariables.Select(variable => new
                    {
                        uuid = variable.Uuid,
                        key = variable.Key,
                        value = variable.Value
                    })));

            // when
            IEnumerable<EnvironmentVariable> actualEnvironmentVariables =
                await this.clientBroker.Client.CoolifyServices.ModifyBulkServiceEnvVarsAsync(
                    serviceUuid, inputEnvironmentVariables);

            // then
            actualEnvironmentVariables.Should().HaveCount(2);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/envs/bulk").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
