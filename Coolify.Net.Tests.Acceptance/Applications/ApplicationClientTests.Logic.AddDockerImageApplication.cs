// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.Applications;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.Applications
{
    public partial class ApplicationClientTests
    {
        [Fact]
        public async Task ShouldAddDockerImageApplicationAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string applicationName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/applications/dockerimage").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = applicationUuid,
                        name = applicationName,
                        docker_image = "nginx:latest"
                    }));

            var inputApplication = new Application
            {
                Name = applicationName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                DockerImage = "nginx:latest"
            };

            // when
            Application actualApplication =
                await this.clientBroker.Client.Applications.AddDockerImageApplicationAsync(inputApplication);

            // then
            actualApplication.Uuid.Should().Be(applicationUuid);
            actualApplication.DockerImage.Should().Be("nginx:latest");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/applications/dockerimage").UsingPost())
                .Should().ContainSingle();
        }
    }
}
