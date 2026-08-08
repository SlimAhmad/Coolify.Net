// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Deployments;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Deployments
{
    public class DeploymentClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public DeploymentClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldTriggerRetrieveAndCancelDeploymentAsync()
        {
            // given
            string resourceUuid = Guid.NewGuid().ToString();
            string deploymentUuid = Guid.NewGuid().ToString();

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/deploy").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = deploymentUuid, status = "queued" }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/deployments/{deploymentUuid}").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = deploymentUuid, status = "in_progress" }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/deployments/{deploymentUuid}/cancel").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = deploymentUuid, status = "cancelled_by_user" }));

            // when
            Deployment triggeredDeployment =
                await this.apiFixture.Client.Deployments.DeployByUuidAsync(resourceUuid);

            Deployment retrievedDeployment =
                await this.apiFixture.Client.Deployments.RetrieveDeploymentByUuidAsync(deploymentUuid);

            Deployment cancelledDeployment =
                await this.apiFixture.Client.Deployments.CancelDeploymentAsync(deploymentUuid);

            // then
            triggeredDeployment.Uuid.Should().Be(deploymentUuid);
            retrievedDeployment.Status.Should().Be("in_progress");
            cancelledDeployment.Status.Should().Be("cancelled_by_user");

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/deploy").UsingPost())
                .Should().ContainSingle();
        }

        [Fact]
        public async Task ShouldRetrieveAllDeploymentsAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/deployments").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), status = "in_progress" }
                    }));

            // when
            IEnumerable<Deployment> actualDeployments =
                await this.apiFixture.Client.Deployments.RetrieveAllDeploymentsAsync();

            // then
            actualDeployments.Should().ContainSingle();
        }
    }
}
