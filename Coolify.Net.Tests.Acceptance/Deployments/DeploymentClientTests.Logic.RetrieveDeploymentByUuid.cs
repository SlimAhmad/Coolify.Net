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
        public async Task ShouldRetrieveDeploymentByUuidAsync()
        {
            // given
            string deploymentUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/deployments/{deploymentUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = deploymentUuid, status = "in_progress" }));

            // when
            Deployment actualDeployment =
                await this.clientBroker.Client.Deployments.RetrieveDeploymentByUuidAsync(deploymentUuid);

            // then
            actualDeployment.Status.Should().Be("in_progress");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/deployments/{deploymentUuid}").UsingGet())
                .Should().ContainSingle();
        }
    }
}
