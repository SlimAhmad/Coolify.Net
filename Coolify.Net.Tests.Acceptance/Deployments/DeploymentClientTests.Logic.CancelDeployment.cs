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
        public async Task ShouldCancelDeploymentAsync()
        {
            // given
            string deploymentUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/deployments/{deploymentUuid}/cancel").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = deploymentUuid, status = "cancelled_by_user" }));

            // when
            Deployment actualDeployment =
                await this.clientBroker.Client.Deployments.CancelDeploymentAsync(deploymentUuid);

            // then
            actualDeployment.Status.Should().Be("cancelled_by_user");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/deployments/{deploymentUuid}/cancel").UsingPost())
                .Should().ContainSingle();
        }
    }
}
