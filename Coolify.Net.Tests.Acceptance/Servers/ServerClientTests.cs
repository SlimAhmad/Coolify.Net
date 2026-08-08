// ---------------------------------------------------------------
// Copyright (c) Coolify.Net Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Coolify.Net.Tests.Acceptance.Brokers;

namespace Coolify.Net.Tests.Acceptance.Servers
{
    [Collection(nameof(ClientTestCollection))]
    public partial class ServerClientTests
    {
        private readonly ClientBroker clientBroker;

        public ServerClientTests(ClientBroker clientBroker)
        {
            this.clientBroker = clientBroker;
            this.clientBroker.Reset();
        }

        private static string GetRandomString() => Guid.NewGuid().ToString();
    }
}
