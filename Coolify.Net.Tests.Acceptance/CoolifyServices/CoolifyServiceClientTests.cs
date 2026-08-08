// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Models.Foundations.EnvironmentVariables;
using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.CoolifyServices
{
    [Collection(nameof(ClientTestCollection))]
    public partial class CoolifyServiceClientTests
    {
        private readonly ClientBroker clientBroker;

        public CoolifyServiceClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();

        private static EnvironmentVariable CreateRandomEnvironmentVariable() =>
            new EnvironmentVariable { Key = GetRandomString(), Value = GetRandomString() };
    }
}
