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
    public partial class CoolifyServiceClientTests
    {
        [Fact]
        public async Task ShouldAddCoolifyServiceAsync()
        {
            // given
            string serviceUuid = GetRandomString();
            string serviceName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath("/api/v1/services").UsingPost())
                .RespondWith(Response.Create()
                    .WithStatusCode(201)
                    .WithBodyAsJson(new { uuid = serviceUuid, name = serviceName }));

            var inputService = new CoolifyService
            {
                Name = serviceName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString()
            };

            // when
            CoolifyService actualService =
                await this.clientBroker.Client.CoolifyServices.AddCoolifyServiceAsync(inputService);

            // then
            actualService.Uuid.Should().Be(serviceUuid);
            actualService.Name.Should().Be(serviceName);

            var postRequest = this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath("/api/v1/services").UsingPost())
                .Single().RequestMessage;

            postRequest.Headers!["Authorization"].Should()
                .ContainSingle(header => header == $"Bearer {ClientBroker.ApiToken}");
        }
    }
}
