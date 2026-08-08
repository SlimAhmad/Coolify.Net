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
        public async Task ShouldDeployByUuidAsync()
        {
            // given
            string resourceUuid = GetRandomString();
            string deploymentUuid = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/deploy").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new { uuid = deploymentUuid, status = "queued" }));

            // when
            Deployment actualDeployment =
                await this.clientBroker.Client.Deployments.DeployByUuidAsync(resourceUuid);

            // then
            actualDeployment.Uuid.Should().Be(deploymentUuid);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/deploy").UsingPost())
                .Should().ContainSingle();
        }
    }
}
