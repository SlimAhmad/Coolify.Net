// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Deployments;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Deployments
{
    public partial class DeploymentClientTests
    {
        [Fact]
        public async Task ShouldRetrieveApplicationDeploymentsAsync()
        {
            // given
            string applicationUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/deployments/applications/{applicationUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = GetRandomString(), status = "finished" }
                    }));

            // when
            IEnumerable<Deployment> actualDeployments =
                await this.clientBroker.Client.Deployments.RetrieveApplicationDeploymentsAsync(applicationUuid);

            // then
            actualDeployments.Should().ContainSingle();

            this.clientBroker.Server
                .FindLogEntries(
                    Request.Create().WithPath($"/api/v1/deployments/applications/{applicationUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
