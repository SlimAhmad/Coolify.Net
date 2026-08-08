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
        public async Task ShouldAddDockerfileApplicationAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string applicationName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/applications/dockerfile").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new
                    {
                        uuid = applicationUuid,
                        name = applicationName,
                        dockerfile_location = "/Dockerfile"
                    }));

            var inputApplication = new Application
            {
                Name = applicationName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString(),
                DockerfileLocation = "/Dockerfile"
            };

            // when
            Application actualApplication =
                await this.clientBroker.Client.Applications.AddDockerfileApplicationAsync(inputApplication);

            // then
            actualApplication.Uuid.Should().Be(applicationUuid);
            actualApplication.DockerfileLocation.Should().Be("/Dockerfile");

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/applications/dockerfile").UsingPost())
                .Should().ContainSingle();
        }
    }
}
