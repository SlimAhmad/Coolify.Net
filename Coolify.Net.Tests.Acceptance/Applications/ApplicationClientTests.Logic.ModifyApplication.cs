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
        public async Task ShouldModifyApplicationAsync()
        {
            // given
            string applicationUuid = GetRandomString();
            string modifiedName = GetRandomString();

            this.clientBroker.Server
                .Given(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}").UsingPatch())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(new
                    {
                        uuid = applicationUuid,
                        name = modifiedName
                    }));

            var inputApplication = new Application
            {
                Uuid = applicationUuid,
                Name = modifiedName,
                ServerUuid = GetRandomString(),
                ProjectUuid = GetRandomString()
            };

            // when
            Application actualApplication =
                await this.clientBroker.Client.Applications.ModifyApplicationAsync(inputApplication);

            // then
            actualApplication.Uuid.Should().Be(applicationUuid);
            actualApplication.Name.Should().Be(modifiedName);

            this.clientBroker.Server
                .FindLogEntries(Request.Create().WithPath($"/api/v1/applications/{applicationUuid}").UsingPatch())
                .Should().ContainSingle();
        }
    }
}
