// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.CoolifyServices;
using Coolify.Net.Tests.Acceptance.Brokers;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Coolify.Net.Tests.Acceptance.CoolifyServices
{
    public class CoolifyServiceClientAcceptanceTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture apiFixture;

        public CoolifyServiceClientAcceptanceTests(ApiFixture apiFixture)
        {
            this.apiFixture = apiFixture;
            this.apiFixture.Reset();
        }

        [Fact]
        public async Task ShouldProvisionStartAndRemoveServiceAsync()
        {
            // given
            string serviceUuid = Guid.NewGuid().ToString();
            string serviceName = "acceptance-service";

            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/services").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new { uuid = serviceUuid, name = serviceName, type = "plausible" }));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}/start").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(200));

            this.apiFixture.Server
                .Given(Request.Create().WithPath($"/api/v1/services/{serviceUuid}").UsingDelete())
                .RespondWith(Response.Create().WithStatusCode(200));

            var newService = new CoolifyService
            {
                Name = serviceName,
                ServiceType = "plausible",
                ServerUuid = Guid.NewGuid().ToString(),
                ProjectUuid = Guid.NewGuid().ToString()
            };

            // when
            CoolifyService addedService =
                await this.apiFixture.Client.CoolifyServices.AddCoolifyServiceAsync(newService);

            await this.apiFixture.Client.CoolifyServices.StartServiceAsync(serviceUuid);
            await this.apiFixture.Client.CoolifyServices.RemoveCoolifyServiceAsync(serviceUuid);

            // then
            addedService.Uuid.Should().Be(serviceUuid);
            addedService.ServiceType.Should().Be("plausible");

            this.apiFixture.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/services/{serviceUuid}").UsingDelete())
                .Should().ContainSingle();
        }

        [Fact]
        public async Task ShouldRetrieveAllServicesAsync()
        {
            // given
            this.apiFixture.Server
                .Given(Request.Create().WithPath("/api/v1/services").UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new[]
                    {
                        new { uuid = Guid.NewGuid().ToString(), name = "service-one" }
                    }));

            // when
            IEnumerable<CoolifyService> actualServices =
                await this.apiFixture.Client.CoolifyServices.RetrieveAllServicesAsync();

            // then
            actualServices.Should().ContainSingle();
        }
    }
}
